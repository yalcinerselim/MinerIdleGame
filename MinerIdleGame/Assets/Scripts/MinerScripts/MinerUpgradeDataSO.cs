using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MinerUpgradeDataSO", menuName = "Scriptable Objects/MinerUpgradeDataSO")]
public class MinerUpgradeDataSO : ScriptableObject, ISaveable
{
    public event Action CurrentCostUpdated; 

    [SerializeField] private string saveID;
    
    private float _currentCost = 10f;
    private float _costMultiplier = 2f;

    private void OnEnable()
    {
        CurrentCostUpdated = null;
    }

    public string GetSaveID()
    {
        return saveID;
    }

    public string GetSaveData()
    {
        return _currentCost.ToString();
    }

    public void LoadFromSaveData(string savedData)
    {
        if (float.TryParse(savedData, out float savedCost))
        {
            _currentCost = savedCost;
            CurrentCostUpdated?.Invoke();
        }
    }

    [SerializeField] private float defaultCost = 10f;

    public void ResetData()
    {
        _currentCost = defaultCost;
        CurrentCostUpdated?.Invoke();
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
