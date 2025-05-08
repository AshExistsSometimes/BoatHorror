using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private Outline outline;

    public string message;

    public UnityEvent onInteract;

    private SceneManager sceneManager;

    // // // // // // // // // // //

    private void Awake()
    {
        sceneManager = FindObjectOfType<SceneManager>();
    }

    private void Start()
    {
        outline = GetComponent<Outline>();
        EnableOutline();// Ensures all the outlines are loaded to prevent stuttering when player hovers over an interactable the first time
    }

    private void Update()
    {
        if (!sceneManager.isLoading)// Removes outlines before player leaves loading screen
        {
            DisableOutline();
        }
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
