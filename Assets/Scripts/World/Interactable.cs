using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private Outline outline;

    public string message;

    public UnityEvent onInteract;

    // // // // // // // // // // //

    private void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        onInteract.Invoke();
    }
    

    // Outline Enabling & Disabling
    public void DisableOutline()
    {
        outline.enabled = false;
    }
    public void EnableOutline()
    {
        outline.enabled = true;
    }
}
