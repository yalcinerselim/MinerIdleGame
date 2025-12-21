using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Referanslar")]
    [SerializeField] private ResourceDataSO moneyData;
    [SerializeField] private MiningController miningController;
    [SerializeField] private TextMeshProUGUI costText;

    [Header("Ayarlar")] 
    [SerializeField] private float currentCost = 10f;
    [SerializeField] private float costMultiplier = 2f;
    [SerializeField] private float powerIncrement = 1f;

    private void Start()
    {
        UpdateUI();
    }

    public void TryPurchase()
    {
        if (moneyData.Amount >= currentCost)
        {
            moneyData.Add(-currentCost);
            
            miningController.UpgradeMiningRate(powerIncrement);
            
            currentCost *= costMultiplier;
            
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        costText.text = "Boost\n ( " +  currentCost.ToString("F0") + " M )";
    }
}
