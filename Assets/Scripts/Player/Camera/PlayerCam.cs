using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float xSens;
    public float ySens;

    public Transform orientation;

    float xRot;
    float yRot;

    public bool CursorLocked = true;


    private void Start()
    {
        CursorLocked = true;
    }

    private void Update()
    {
        CursorLockCheck();

        // Mouse Input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySens;

        yRot += mouseX;

        xRot -= mouseY;

        xRot = Mathf.Clamp(xRot, -89.9f, 89.9f);

        // Camera Rotation + Orientation
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }

    //////////////////////////////////////


    private void CursorLockCheck()
    {
        if (CursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }// Checks if CursorLocked is true, and locks cursor if that is the case | Unlocks cursor if false
}
