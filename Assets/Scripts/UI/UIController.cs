using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    private void Update()
    {
        ScreenFilterEnabledCheck();
    }

    private void Awake()
    {
        Instance = this;
    }

    [Header("Interaction Popup Text")]////////////////////////////////

    [SerializeField] TMP_Text InteractionText;

    public void EnableInteractionText(string text)
    {
        InteractionText.text = text + " (E)";
        InteractionText.gameObject.SetActive(true);
    }
    public void DisableInteractionText()
    {
        InteractionText.gameObject.SetActive(false);
    }

    
    [Header("Screen Filter")]/////////////////////////////////////////

    public bool ScreenFilterEnabled = true;
    public GameObject ScreenFilter;
    private void ScreenFilterEnabledCheck()
    {
        if (ScreenFilterEnabled)
        {
            ScreenFilter.SetActive(true);
        }
        else if (!ScreenFilterEnabled)
        {
            ScreenFilter.SetActive(false);
        }
    }
}
