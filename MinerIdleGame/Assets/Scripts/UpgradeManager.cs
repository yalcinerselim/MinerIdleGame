using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("Referanslar")]
    [SerializeField] private ResourceDataSO moneyData;
    [SerializeField] private MiningController miningController;
    [SerializeField] private TextMeshProUGUI costText;
    
    [SerializeField] private Button myButton;

    [Header("Ayarlar")] 
    [SerializeField] private float currentCost = 10f;
    [SerializeField] private float costMultiplier = 2f;
    [SerializeField] private float powerIncrement = 1f;

    private void Awake()
    {
        if (myButton == null) myButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        moneyData.OnValueChanged += CheckAffordability;
        
        UpdateUI();
        CheckAffordability(moneyData.Amount);
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
            
            miningController.UpgradeMiningRate(powerIncrement);
            
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        costText.text = "Boost\n ( " +  currentCost.ToString("F0") + " M )";
    }

    public float GetCurrentCost()
    {
        return currentCost;
    }

    public void Load(float savedCost)
    {
        currentCost = savedCost;
        UpdateUI();
    }
}
