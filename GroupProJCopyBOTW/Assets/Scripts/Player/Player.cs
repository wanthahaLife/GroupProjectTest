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

    bool isMoveInput = false;

    PlayerInputActions inputActions;
    Vector3 inputDir = Vector3.zero;
    Transform character;
    Animator animator;

    PlayerSkillController skillController;
    public PlayerSkillController SkillController => skillController;

    public Action rightClick;
    public Action leftClick;
    public Action<int> skillSelect;

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
        inputActions.Player.Skill.performed += OnSkillSelect;
    }


    private void OnDisable()
    {
        inputActions.Player.Skill.performed -= OnSkillSelect;
        inputActions.Player.RightClick.performed -= OnRightClick;
        inputActions.Player.LeftClick.performed -= OnLeftClick;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }
    private void OnSkillSelect(InputAction.CallbackContext context)
    {
        skillSelect?.Invoke(context.ReadValue<int>());
    }

    private void OnRightClick(InputAction.CallbackContext _)
    {
        rightClick?.Invoke();
    }

    private void OnLeftClick(InputAction.CallbackContext _)
    {
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
