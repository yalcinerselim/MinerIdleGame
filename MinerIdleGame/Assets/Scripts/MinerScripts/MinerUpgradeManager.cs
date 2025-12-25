using UnityEngine;
using UnityEngine.UI;

public class MinerUpgradeManager : MonoBehaviour
{
    [Header("Referanslar")]
    [SerializeField] private ResourceDataSO moneyData;
    [SerializeField] private MinerData minerData;
    
    [SerializeField] private Button myButton;

    [Header("Ayarlar")] 
    [SerializeField] private MinerUpgradeDataSO minerUpgradeData;

    private void Awake()
    {
        if (myButton == null) myButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        moneyData.OnValueChanged += CheckAffordability;
        
        CheckAffordability(moneyData.Amount);
    }

    private void OnDisable()
    {
        moneyData.OnValueChanged -= CheckAffordability;
    }

    private void CheckAffordability(float currentMoney)
    {
        myButton.interactable = (currentMoney >= minerUpgradeData.GetCurrentCost());
    }

    public void TryPurchase()
    {
        if (moneyData.Amount >= minerUpgradeData.GetCurrentCost())
        {
            var oldCost = minerUpgradeData.GetCurrentCost();
            minerUpgradeData.UpdateCurrentCost();
            
            moneyData.Add(-oldCost);
            
            minerData.UpdateLevel(1);
        }
    }
}
