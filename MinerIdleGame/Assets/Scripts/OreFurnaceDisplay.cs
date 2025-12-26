using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OreFurnaceDisplay : MonoBehaviour
{
    [SerializeField] private OreFurnace oreFurnaceController;
    [SerializeField] private Button myButton;
    [SerializeField] private TextMeshProUGUI buttonText;
    
    private void Awake()
    {
        myButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        oreFurnaceController.FurnaceStateChanged += ChangeButtonColor;
        oreFurnaceController.FurnaceStateChanged += ChangeButtonText;
    }

    private void OnDisable()
    {
        oreFurnaceController.FurnaceStateChanged -= ChangeButtonColor;
        oreFurnaceController.FurnaceStateChanged -= ChangeButtonText;
    }

    private void ChangeButtonColor(bool furnaceState)
    {
        if (furnaceState)
        {
            myButton.image.color = Color.green; // Aktif renk
        }
        else
        {
            myButton.image.color = Color.grey; // Pasif renk
        }
    }

    private void ChangeButtonText(bool furnaceState)
    {
        if (furnaceState)
        {
            buttonText.text = "Stop Furnace";
        }
        else
        {
            buttonText.text = "Start Furnace";
        }
    }

}
