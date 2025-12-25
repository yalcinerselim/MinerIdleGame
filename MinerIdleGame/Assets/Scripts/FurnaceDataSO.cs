using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FurnaceDataSO", menuName = "Scriptable Objects/FurnaceDataSO")]
public class FurnaceDataSO : ScriptableObject
{
    [SerializeField] private float _smeltingRate = 1f;
    [SerializeField] private ResourceDataSO moneyData;
    [SerializeField] private Button myButton;

    private float _currentCost = 250;
    
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
        myButton.interactable = (currentMoney >= _currentCost);
    }
    
    public float GetSmeltingRate()
    {
        return _smeltingRate;
    }
    
    public void UpgradeFurnace()
    {
        _smeltingRate += 1f;
    }

    public void Load(float savedSmeltingRate)
    {
        _smeltingRate = savedSmeltingRate;
    }
}
