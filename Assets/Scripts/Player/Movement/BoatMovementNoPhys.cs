using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovementNoPhys : MonoBehaviour
{
    private Vector3 MovementInput;

    [SerializeField] private Vector3 velocity;

    [SerializeField] private CharacterController Controller;

    [Space]
    public float Speed = 4f;
    public float TurnSpeed = 7f;

    void Start()
    {
        
    }
    void Update()
    {
        MovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        MoveBoat();
        TurnBoat();
    }

    private void MoveBoat()
    {
        Vector3 MoveVector = transform.TransformDirection(MovementInput);
        Controller.Move(MoveVector * Speed * Time.deltaTime);
        Controller.Move(velocity * Time.deltaTime);
    }

    private void TurnBoat()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //Turn Left
            transform.Rotate(0.0f, -TurnSpeed * Time.deltaTime, 0.0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Turn Right
            transform.Rotate(0.0f, TurnSpeed * Time.deltaTime, 0.0f);
        }
    }
}

