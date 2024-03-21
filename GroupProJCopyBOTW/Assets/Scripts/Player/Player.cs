using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float rotateSpeed = 2.0f;
    public float throwPower = 5.0f;

    bool isMoveInput = false;

    PlayerInputActions inputActions;
    Vector3 inputDir = Vector3.zero;
    Transform character;
    Transform pickUpRoot;
    Animator animator;

    PlayerSkillController skillController;
    HandRootTracker handRootTracker;
    public PlayerSkillController SkillController => skillController;

    public Action rightClick;
    public Action leftClick;
    public Action<SkillName> onSkillSelect;
    public Action onSkill;
    Action onThrow;
    public Action onPickUp;
    public Action onCancel;

    SkillName selectSkill = SkillName.RemoteBomb;
    SkillName SelectSkill
    {
        get => selectSkill;
        set
        {
            if (selectSkill != value)
            {
                selectSkill = value;
                Debug.Log($"스킬 [{selectSkill}]로 설정");
                onSkillSelect?.Invoke(selectSkill);
            }
        }
    }

    readonly int Hash_IsMove = Animator.StringToHash("IsMove");
    readonly int Hash_IsPickUp = Animator.StringToHash("IsPickUp");
    readonly int Hash_Throw = Animator.StringToHash("Throw");

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        character = transform.GetChild(0);
        animator = character.GetComponent<Animator>();
        skillController = transform.GetComponent<PlayerSkillController>();

        handRootTracker = transform.GetComponentInChildren<HandRootTracker>();
        HandRoot handRoot = transform.GetComponentInChildren<HandRoot>();

        pickUpRoot = transform.GetChild(2);
        pickUpPoint = pickUpRoot.position;
        pickUpPoint.y += pickUpHeightRange;

        rightClick += PickUpObject;
        onCancel += DropObject;
        onThrow += ThrowObject;

        onPickUp += () => handRootTracker.OnTracking(handRoot.transform);
        onSkill += () => handRootTracker.OnTracking(handRoot.transform);
        onCancel += handRootTracker.OffTracking;
    }


    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;

        inputActions.Player.LeftClick.performed += OnLeftClick;
        inputActions.Player.RightClick.performed += OnRightClick;

        inputActions.Player.OnSkill.performed += OnSkill;
        inputActions.Player.Skill1.performed += OnSkill1;
        inputActions.Player.Skill2.performed += OnSkill2;
        inputActions.Player.Skill3.performed += OnSkill3;
        inputActions.Player.Skill4.performed += OnSkill4;
        inputActions.Player.Skill5.performed += OnSkill5;

        inputActions.Player.Throw.performed += OnThrow;
        inputActions.Player.Cancel.performed += OnCancel;
    }



    private void OnDisable()
    {
        inputActions.Player.Cancel.performed -= OnCancel;
        inputActions.Player.Throw.performed -= OnThrow;

        inputActions.Player.Skill5.performed -= OnSkill5;
        inputActions.Player.Skill4.performed -= OnSkill4;
        inputActions.Player.Skill3.performed -= OnSkill3;
        inputActions.Player.Skill2.performed -= OnSkill2;
        inputActions.Player.Skill1.performed -= OnSkill1;
        inputActions.Player.OnSkill.performed -= OnSkill;

        inputActions.Player.RightClick.performed -= OnRightClick;
        inputActions.Player.LeftClick.performed -= OnLeftClick;

        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    public float liftRadius = 0.5f;
    public float pickUpHeightRange = 0.5f;
    Vector3 pickUpPoint = Vector3.zero;
    bool isPickUp = false;
    bool IsPickUp
    {
        get => isPickUp;
        set
        {
            isPickUp = value;
            animator.SetBool(Hash_IsPickUp, isPickUp);
        }
    }
    ReactionObject reaction;

    void PickUpObject()
    {
        if (!IsPickUp)    // 맨손일 때 만 가능하도록 조건 넣기
        {
            Collider[] hit = Physics.OverlapCapsule(pickUpRoot.position, pickUpPoint, liftRadius);
            for(int i = 0; i < hit.Length; i++)
            {
                reaction = hit[i].transform.GetComponent<ReactionObject>();
                if (reaction != null && (reaction.Type & ReactionType.Throw) != 0)
                {
                    IsPickUp = true;
                    onPickUp?.Invoke();
                    reaction.PickUp(handRootTracker.transform);
                    reaction.transform.rotation = Quaternion.identity;
                    break;
                }
            }
        }
    }

    void ThrowObject()
    {
        if (IsPickUp && reaction != null)
        {
            animator.SetTrigger(Hash_Throw);
            reaction.Throw(throwPower, transform);
            IsPickUp = false;
            reaction = null;
        }
    }
    void DropObject()
    {
        if (IsPickUp)    // 맨손일 때 만 가능하도록 조건 넣기
        {
            IsPickUp = false;
            reaction.Drop();
        }
    }

    private void OnSkill(InputAction.CallbackContext _)
    {
        switch (selectSkill)
        {
            case SkillName.RemoteBomb:
                //animator.SetBool("Hash_IsThrowStart", true);
                break;
        }
        onSkill?.Invoke();
    }

    private void OnSkill1(InputAction.CallbackContext _)
    {
        //if (isSKillMenuOn)
        {
            SelectSkill = SkillName.RemoteBomb;
        }
    }
    private void OnSkill2(InputAction.CallbackContext _)
    {
        //if (isSKillMenuOn)
        {
            SelectSkill = SkillName.RemoteBomb_Cube;
        }
    }
    private void OnSkill3(InputAction.CallbackContext _)
    {
        //if (isSKillMenuOn)
        {
            SelectSkill = SkillName.MagnetCatch;
        }
    }
    private void OnSkill4(InputAction.CallbackContext _)
    {
        //if (isSKillMenuOn)
        {
            SelectSkill = SkillName.IceMaker;
        }
    }
    private void OnSkill5(InputAction.CallbackContext context)
    {
        //if (isSKillMenuOn)
        {
            SelectSkill = SkillName.TimeLock;
        }
    }

    private void OnRightClick(InputAction.CallbackContext _)
    {
        switch (selectSkill)
        {
            case SkillName.RemoteBomb:
                //animator.SetBool("Hash_IsThrowStart", false);
                break;
        }
        rightClick?.Invoke();
    }

    private void OnLeftClick(InputAction.CallbackContext _)
    {
        switch (selectSkill)
        {
            case SkillName.RemoteBomb:
                //animator.SetTrigger("Hash_Throw");
                break;
        }
        leftClick?.Invoke();
    }
    private void OnThrow(InputAction.CallbackContext context)
    {
        onThrow?.Invoke();
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        onCancel?.Invoke(); 
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector3 tempDir = context.ReadValue<Vector2>();
        inputDir.x = tempDir.x;
        inputDir.y = 0f;
        inputDir.z = tempDir.y;
        isMoveInput = !context.canceled;
        animator.SetBool(Hash_IsMove, !context.canceled);
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * inputDir);
        if (inputDir.sqrMagnitude > 0.001f)
        {
            character.rotation = Quaternion.Slerp(character.rotation, Quaternion.LookRotation(inputDir) * transform.rotation, rotateSpeed * Time.deltaTime);
            //character.rotation = Quaternion.LookRotation(inputDir) * transform.rotation;
        }
        if(isMoveInput)
        {
            LookForwardPlayer(Camera.main.transform.forward);
        }
    }

    void LookForwardPlayer(Vector3 rotate)
    {
        //rotate.x = 0;
        //rotate.z = 0;
        rotate.y = 0;
        transform.forward = rotate;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward);
    }

#endif
}
