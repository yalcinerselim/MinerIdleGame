using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MinerUpgradeDataSO", menuName = "Scriptable Objects/MinerUpgradeDataSO")]
public class MinerUpgradeDataSO : ScriptableObject
{
    public event Action CurrentCostUpdated; 
    
    private float _currentCost = 10f;
    private float _costMultiplier = 2f;

    private void OnEnable()
    {
        CurrentCostUpdated = null;
    }

    public void UpdateCurrentCost()
    {
        _currentCost *= _costMultiplier;
        CurrentCostUpdated?.Invoke();
    }

    public float GetCurrentCost()
    {
        return _currentCost;
    }

    public void Load(float savedCost)
    {
        _currentCost = savedCost;
        CurrentCostUpdated?.Invoke();
    }
    
}
