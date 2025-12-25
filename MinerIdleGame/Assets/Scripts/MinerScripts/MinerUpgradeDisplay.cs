using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinerUpgradeDisplay : MonoBehaviour
{
    [SerializeField] private MinerUpgradeDataSO minerUpgradeData;
    
    [SerializeField] private TextMeshProUGUI _currentCostText;

    [SerializeField] private string prefix = "";
    private void OnEnable()
    {
        minerUpgradeData.CurrentCostUpdated += UpdateUI;
    }

    private void UpdateUI()
    {
        _currentCostText.text = prefix + "\n ( " + minerUpgradeData.GetCurrentCost().ToString("F0") + " M )";
    }
}
