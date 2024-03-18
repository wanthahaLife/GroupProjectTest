using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float rotateSpeed = 2.0f;
    public float throwPower = 5.0f;

    bool isMoveInput = false;

    PlayerInputActions inputActions;
    Vector3 inputDir = Vector3.zero;
    Transform character;
    Animator animator;

    PlayerSkillController skillController;
    public PlayerSkillController SkillController => skillController;

    public Action rightClick;
    public Action leftClick;
    public Action<SkillName> onSkillSelect;
    public Action onSkill;
    public Action leftUp;
    public Action leftDown;
    public Action<float> onThrow;

    readonly int Hash_IsMove = Animator.StringToHash("IsMove");

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        character = transform.GetChild(0);
        animator = character.GetComponent<Animator>();
        skillController = transform.GetComponent<PlayerSkillController>();
    }


    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.LeftClick.performed += OnLeftClick;
        inputActions.Player.RightClick.performed += OnRightClick;
        inputActions.Player.Skill.performed += OnSkill;
        inputActions.Player.leftUp.performed += LeftUp;
        inputActions.Player.leftDown.performed += LeftDown;
        inputActions.Player.Skill1.performed += OnSkill1;
        inputActions.Player.Skill2.performed += OnSkill2;
        inputActions.Player.Skill3.performed += OnSkill3;
        inputActions.Player.Skill4.performed += OnSkill4;
        inputActions.Player.Throw.performed += OnThrow;
    }


    private void OnDisable()
    {
        inputActions.Player.Throw.performed -= OnThrow;
        inputActions.Player.Skill4.performed -= OnSkill4;
        inputActions.Player.Skill3.performed -= OnSkill3;
        inputActions.Player.Skill2.performed -= OnSkill2;
        inputActions.Player.Skill1.performed -= OnSkill1;
        inputActions.Player.leftDown.performed -= LeftDown;
        inputActions.Player.leftUp.performed -= LeftUp;
        inputActions.Player.Skill.performed -= OnSkill;
        inputActions.Player.RightClick.performed -= OnRightClick;
        inputActions.Player.LeftClick.performed -= OnLeftClick;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }


    private void OnThrow(InputAction.CallbackContext context)
    {
        onThrow?.Invoke(throwPower);
    }

    void ThrowObject()
    {
        float distance = 2.0f;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out RaycastHit hit ,distance);


    }

    private void OnSkill(InputAction.CallbackContext _)
    {
        switch (selectSkill)
        {
            case SkillName.RemoteBomb:
                animator.SetBool("Hash_IsThrowStart", true);
                break;
        }
        onSkill?.Invoke();
    }

    SkillName selectSkill = SkillName.RemoteBomb;
    SkillName SelectSkill
    {
        get => selectSkill;
        set
        {
            if (selectSkill != value)
            {
                selectSkill = value;
                onSkillSelect?.Invoke(selectSkill);
            }
            IsSkillMenuOn = false;
        }
    }

    readonly int Hash_IsThrowStart = Animator.StringToHash("IsThrowStart");
    readonly int Hash_Throw = Animator.StringToHash("Throw");

    private void OnSkill1(InputAction.CallbackContext _)
    {
        if (isSKillMenuOn)
        {
            SelectSkill = SkillName.RemoteBomb;
        }
    }
    private void OnSkill2(InputAction.CallbackContext _)
    {
        if (isSKillMenuOn)
        {
            SelectSkill = SkillName.MagnetCatch;
        }
    }
    private void OnSkill3(InputAction.CallbackContext _)
    {
        if (isSKillMenuOn)
        {
            SelectSkill = SkillName.IceMaker;
        }
    }
    private void OnSkill4(InputAction.CallbackContext _)
    {
        if (isSKillMenuOn)
        {
            SelectSkill = SkillName.TimeLock;
        }
    }

    bool isSKillMenuOn = false;
    bool IsSkillMenuOn
    {
        get => isSKillMenuOn;
        set
        {
            isSKillMenuOn = value;
            if (isSKillMenuOn)
            {
                Debug.Log("스킬창 On");
            }
            else
            {
                Debug.Log("스킬창 Off");
            }
        }
    }
    private void LeftUp(InputAction.CallbackContext _)
    {
        IsSkillMenuOn = !IsSkillMenuOn;
        leftUp?.Invoke();
    }
    private void LeftDown(InputAction.CallbackContext context)
    {
        leftDown?.Invoke();
    }

    private void OnRightClick(InputAction.CallbackContext _)
    {
        switch (selectSkill)
        {
            case SkillName.RemoteBomb:
                animator.SetBool("Hash_IsThrowStart", false);
                break;
        }
        rightClick?.Invoke();
    }

    private void OnLeftClick(InputAction.CallbackContext _)
    {
        switch (selectSkill)
        {
            case SkillName.RemoteBomb:
                animator.SetTrigger("Hash_Throw");
                break;
        }
        leftClick?.Invoke();
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
}
