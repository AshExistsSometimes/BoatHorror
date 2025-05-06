using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float SpinSpeedX = 0.0f;
    public float SpinSpeedY = 0.0f;
    public float SpinSpeedZ = 0.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(SpinSpeedX * Time.deltaTime, SpinSpeedY * Time.deltaTime, SpinSpeedZ * Time.deltaTime);
    }
}
