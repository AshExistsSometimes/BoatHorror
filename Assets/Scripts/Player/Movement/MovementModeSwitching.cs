using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class MovementModeSwitching : MonoBehaviour
{
    public GameObject Player;
    public GameObject Boat;
    public BoatMovement boatMovement;

    [Space]
    public Transform SeatPoint;
    public float speedToSeat = 5f;

    [Space]
    public UnityEvent StartSailing;
    public UnityEvent StopSailing;

    [Space]
    public bool PlayerSailing = false;

    [Space]
    private float sailingInputCooldown = 0.2f;
    private float sailingInputTimer = 0f;

    //  Track boat position
    private Vector3 lastBoatPosition;
    private Transform boatTransform;

    private void Start()
    {
        boatTransform = Boat.transform;
    }

    public void toggleSailing()
    {
        if (PlayerSailing)
        {
            ExitSailingState();
        }
        else if (!PlayerSailing)
        {
            EnterSailingState();
        }
    }

    private void Update()
    {
        if (PlayerSailing)
        {
            sailingInputTimer += Time.deltaTime; // Prevents 'E' Press from registering twice in one frame

            // Teleport player to the seat position every frame
            Player.transform.position = SeatPoint.position;

            // Calculate boat's movement delta
            Vector3 boatMovementDelta = boatTransform.position - lastBoatPosition;

            // Update player's position based on boat's movement
            // This should still "lock" the player to the boat smoothly
            Player.transform.position += boatMovementDelta;

            lastBoatPosition = boatTransform.position;

            // Exit input handling
            if (sailingInputTimer > sailingInputCooldown &&
                (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
            {
                ExitSailingState();
            }
        }
    }

    

    public void EnterSailingState()
    {
        Debug.Log("Entering Sailing State");
        Player.transform.position = SeatPoint.position;
        boatMovement.enabled = true;
        PlayerSailing = true;

        sailingInputTimer = 0f;

        Player.GetComponent<Rigidbody>().isKinematic = true;

        lastBoatPosition = boatTransform.position;

        StartSailing.Invoke();  
    }

    public void ExitSailingState()
    {
        Debug.Log("Exiting Sailing State");
        boatMovement.enabled = false;
        PlayerSailing = false;


        Player.GetComponent<Rigidbody>().isKinematic = false;

        StopSailing.Invoke();
        Player.transform.position = SeatPoint.position;
    }
}
