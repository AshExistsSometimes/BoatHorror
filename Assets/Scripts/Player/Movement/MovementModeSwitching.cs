using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class MovementModeSwitching : MonoBehaviour
{
    public Transform player;
    public PlayerMovement playerMovement;
    public BoatMovement boatActive;


    public Transform SeatPoint;
    public Transform ExitPoint;

    public UnityEvent StartSailing;
    public UnityEvent StopSailing;

    public float speedToSeat = 5f;

    public bool PlayerSailing = false;


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
            if (PlayerSailing && (Input.GetKeyDown(KeyCode.E) || (Input.GetKeyDown(KeyCode.Escape))))// So the player can leave the sailing state if they arent looking at the wheel
            {
                ExitSailingState();
            }

        if (PlayerSailing)
        {
            player.position = SeatPoint.position;
        }
    }

    public void ExitSailingState()
    {
        boatActive.enabled = false;
        PlayerSailing = false;

        
        playerMovement.enabled = true;

        StopSailing.Invoke();
        player.position = SeatPoint.position;
    }

    public void EnterSailingState()
    {
        boatActive.enabled = true;
        PlayerSailing = true;
        playerMovement.enabled = false;


        StartSailing.Invoke();  
    }
}
