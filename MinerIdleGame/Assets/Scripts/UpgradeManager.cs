using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("Referanslar")]
    [SerializeField] private ResourceDataSO moneyData;
    [SerializeField] private MinerData minerData;
    [SerializeField] private TextMeshProUGUI costText;
    
    [SerializeField] private Button myButton;

    [Header("Ayarlar")] 
    [SerializeField] private float currentCost = 10f;
    [SerializeField] private float costMultiplier = 2f;

    private void Awake()
    {
        if (myButton == null) myButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        moneyData.OnValueChanged += CheckAffordability;
    }

    private void OnDisable()
    {
        moneyData.OnValueChanged -= CheckAffordability;
    }

    private void CheckAffordability(float currentMoney)
    {
        myButton.interactable = (currentMoney >= currentCost);
    }

    public void TryPurchase()
    {
        if (moneyData.Amount >= currentCost)
        {
            var oldCost = currentCost;
            currentCost *= costMultiplier;
            
            moneyData.Add(-oldCost);
            
            minerData.UpgradeMiningRate();
            
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (minerData.isAutomation)
        {
            costText.text = "Boost Automation\n ( " + currentCost.ToString("F0") + " M )";
        }
        else
        {
            costText.text = "Boost\n ( " +  currentCost.ToString("F0") + " M )";
        }
    }

    public float GetCurrentCost()
    {
        return currentCost;
    }

    public void Load(float savedCost)
    {
        currentCost = savedCost;
        UpdateUI();
        CheckAffordability(moneyData.Amount);
    }
}
