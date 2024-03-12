using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float rotateSpeed = 2.0f;


    PlayerInputActions inputActions;
    Vector3 inputDir = Vector3.zero;

    public Action onSkill;
    public Action activatedSkill;
    public Action inactivatedSkill;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.LeftClick.performed += OnLeftClick;
        inputActions.Player.RightClick.performed += OnRightClick;
    }

    private void OnDisable()
    {
        inputActions.Player.RightClick.performed -= OnRightClick;
        inputActions.Player.LeftClick.performed -= OnLeftClick;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void OnRightClick(InputAction.CallbackContext context)
    {
        inactivatedSkill?.Invoke();
    }

    private void OnLeftClick(InputAction.CallbackContext context)
    {
        activatedSkill?.Invoke();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector3 tempDir = context.ReadValue<Vector2>();
        inputDir.x = tempDir.x;
        inputDir.y = 0f;
        inputDir.z = tempDir.y;
    }

    private void Update()
    {
        transform.position = transform.position + Time.deltaTime * moveSpeed * inputDir;
        if (inputDir.sqrMagnitude > 0.001f)
        {
            Quaternion rotate = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputDir), rotateSpeed * Time.deltaTime);
            transform.rotation = rotate;
        }
    }
}
