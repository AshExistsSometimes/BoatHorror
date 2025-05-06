using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisableOnSail : MonoBehaviour
{
    public MovementModeSwitching SailingScript;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (SailingScript.PlayerSailing)
        {
            rb.useGravity = false;
        }
        else if (!SailingScript.PlayerSailing) { rb.useGravity = true; }
    }
}
