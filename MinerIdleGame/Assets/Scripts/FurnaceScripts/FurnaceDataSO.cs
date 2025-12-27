using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FurnaceDataSO", menuName = "Scriptable Objects/FurnaceDataSO")]
public class FurnaceDataSO : ScriptableObject, ISaveable
{
    [SerializeField] private string saveID;
    
    [SerializeField] private float _smeltingRate = 1f;
    [SerializeField] private float _defaultSmeltingRate = 1f;
    [SerializeField] private bool furnaceState;

    private float _furnaceUpdateCost;

    private void OnEnable()
    {
        furnaceState = false;
        _furnaceUpdateCost = 350;
    }

    public float GetSmeltingRate()
    {
        return _smeltingRate;
    }
    
    public void UpgradeFurnace()
    {
        _smeltingRate += 2f;
    }

    public bool GetFurnaceState()
    {
        return furnaceState;
    }
    
    public void SetFurnaceState(bool state)
    {
        furnaceState = state;
    }


    public string GetSaveID()
    {
        return saveID;
    }

    public string GetSaveData()
    {
        return _smeltingRate.ToString();
    }

    public void LoadFromSaveData(string savedData)
    {
        if (float.TryParse(savedData, out float savedSmeltingRate))
        {
            _smeltingRate = savedSmeltingRate;
        }
    }

    public void ResetData()
    {
        _smeltingRate = _defaultSmeltingRate;
    }

    public float GetFurnaceUpdateCost()
    {
        return _furnaceUpdateCost;
    }
}
