using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscilateFlag : MonoBehaviour
{
    public float RotationSpeed = 60f;
    public float RotationAngle = 30f;

    private void Start()
    {
        transform.localEulerAngles = new Vector3(0, -(RotationAngle / 2), 0); 
    }
    private void Update()
    {
        transform.localEulerAngles = new Vector3(0f, Mathf.PingPong(Time.time * RotationSpeed, RotationAngle), 0f);
    }
}
