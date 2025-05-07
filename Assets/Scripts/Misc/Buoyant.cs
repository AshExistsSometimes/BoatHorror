using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyant : MonoBehaviour
{
    
    public Rigidbody rb;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;

    public WaterGeneration waterData;

    private void FixedUpdate()
    {
        if (transform.position.y < 0f)
        {
            float displacementMultiplier = Mathf.Clamp01((-transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            rb.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementAmount, 0f), ForceMode.Acceleration);
        }
    }
}

