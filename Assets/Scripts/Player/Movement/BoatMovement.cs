using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public float SailSpeed = 5.0f;
    public float TurnSpeed = 5.0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -SailSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * (SailSpeed / 2f));
        }
        if (Input.GetKey(KeyCode.A) && ((Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.W)))))
        {
            transform.Rotate(0.0f, -TurnSpeed * Time.deltaTime, 0.0f);
        }
        if (Input.GetKey(KeyCode.D) && ((Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.W)))))
        {
            transform.Rotate(0.0f, TurnSpeed * Time.deltaTime, 0.0f);
        }
    }


}
