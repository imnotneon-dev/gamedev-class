using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    PlayerStats stats;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float sprintSpeed = 10f;

    [SerializeField]
    float jumpForce = 0.5f;

    [SerializeField]
    float gravity = -19.62f;

    [SerializeField]
    float playerFall = -10f;

    float verticalVelocity;
    bool isSprinting;
    Vector2 moveInput;
    CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < playerFall)
        {
            Respawn();
        }

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        verticalVelocity += gravity * Time.deltaTime;

        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        Vector3 velocity = (move * currentSpeed) + (Vector3.up * verticalVelocity);
        controller.Move(velocity * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            if (controller.isGrounded)
            {
                verticalVelocity = Mathf.Sqrt(jumpForce * -1f * gravity);
            }
        }
    }
    public void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
    }
    public void Respawn()
    {
        if (spawnPoint != null)
        {
            controller.enabled = false;

            verticalVelocity = 0f;
            transform.position = spawnPoint.position;

            controller.enabled = true;
        }
    }
}