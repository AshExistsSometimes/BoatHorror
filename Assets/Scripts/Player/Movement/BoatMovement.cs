using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public float SailSpeed = 5.0f;
    public float TurnSpeed = 5.0f;

    [SerializeField] 
    private Vector3 currentVelocity = Vector3.zero;
    private float velocityDrag = 1.5f;
    public float collisionPushForce = 3.0f;

    public Rigidbody capsuleRigidbody; // Reference to the capsule's Rigidbody

    public bool isColliding = false;


    private void Start()
    {
        if (capsuleRigidbody == null)
        {
            Debug.LogError("Capsule Rigidbody not assigned! Please assign it in the inspector.");
        }
    }
    public void ApplyCollisionForce(Vector3 direction)
    {
        currentVelocity = direction.normalized * collisionPushForce;
    }

    public void SetCollisionState(bool state)
    {
        isColliding = state;

        if (isColliding)
        {
            // Set the capsule's rigidbody to be affected by physics when colliding
            capsuleRigidbody.isKinematic = false;
        }
        else
        {
            // Revert back to kinematic when no longer colliding
            capsuleRigidbody.isKinematic = true;
        }
    }

    void Update()
    {
        {
            if (isColliding)
            {
                // **New code**: Apply force in the opposite direction of the boat's velocity to stop it
                if (capsuleRigidbody.velocity.magnitude > 0.1f)
                {
                    Vector3 oppositeDirection = -capsuleRigidbody.velocity.normalized;
                    capsuleRigidbody.AddForce(oppositeDirection * SailSpeed * 2f, ForceMode.Impulse); // Stop force
                }
                return;
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * -SailSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * (SailSpeed / 2f));
            }
            if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))
            {
                transform.Rotate(0.0f, -TurnSpeed * Time.deltaTime, 0.0f);
            }
            if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))
            {
                transform.Rotate(0.0f, TurnSpeed * Time.deltaTime, 0.0f);
            }
        }
    }
}
