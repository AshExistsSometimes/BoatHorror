using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCollisionDetection : MonoBehaviour
{
    public BoatMovement boatMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            Debug.Log("Collided with: " + other.gameObject.name);

            Vector3 direction = transform.position - other.ClosestPoint(transform.position);
            boatMovement.ApplyCollisionForce(direction);  // **New** Call to apply pushback
        }
    }
}
