using System;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceUpgradeManager : MonoBehaviour
{
    [SerializeField] private FurnaceDataSO furnaceData;
    [SerializeField] private ResourceDataSO moneyData;

    [SerializeField] private Button myButton;

    private void OnEnable()
    {
        moneyData.OnValueChanged += CheckAffordability;
    }

    private void OnDisable()
    {
        moneyData.OnValueChanged -= CheckAffordability;
    }

    public void UpgradeFurnace()
    {
        if(!(moneyData.Amount >= furnaceData.GetFurnaceUpdateCost())) return;
        
        moneyData.Add(-furnaceData.GetFurnaceUpdateCost());
        furnaceData.UpgradeFurnace();
    }

    private void CheckAffordability(float currentMoney)
    {
        myButton.interactable = (currentMoney >= furnaceData.GetFurnaceUpdateCost());
    }
}
