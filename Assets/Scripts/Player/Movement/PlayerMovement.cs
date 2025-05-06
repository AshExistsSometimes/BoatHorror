using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 40;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public GameObject player;

    Vector3 moveDirection;

    Rigidbody rb;

    private bool playingSound = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();

        float xVel = rb.velocity.x;
        float zVel = rb.velocity.z;

        float HorizInput = Input.GetAxisRaw("Horizontal");
        if ((xVel > 0.1f || zVel > 0.1f) && HorizInput > 0.1f && !playingSound)
        {
            //Movement detection
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }
}
