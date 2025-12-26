using System;
using UnityEngine;

public class OreFurnace : MonoBehaviour
{
    [SerializeField] private FurnaceDataSO furnaceData;
    
    [SerializeField] private ResourceDataSO oreData;
    [SerializeField] private ResourceDataSO outputData;
    [SerializeField] private ResourceDataSO moneyData;
    
    public bool isFurnaceOn;
    private float _timer;

    public event Action<bool> FurnaceStateChanged;

    private void OnEnable()
    {
        isFurnaceOn = false;
        FurnaceStateChanged?.Invoke(isFurnaceOn);
    }

    private void Update()
    {
        if (isFurnaceOn)
        {
            _timer += Time.deltaTime;
            if (_timer >= 1f)
            {
                _timer = 0f;
                SmeltOre();
            }
        }
    }

    public void TurnOnOffFurnace()
    {
        if (isFurnaceOn)
        {
            TurnOffFurnace();
        }
        else
        {
            TurnOnFurnace();
        }
    }

    private void TurnOnFurnace()
    {
        if (oreData.Amount >= furnaceData.GetSmeltingRate())
        {
            ChangeFurnaceState(true);
        }
    }

    private void TurnOffFurnace()
    {
        ChangeFurnaceState(false);
    }

    private void ChangeFurnaceState(bool state)
    {
        isFurnaceOn = state;
        furnaceData.SetFurnaceState(state);
        FurnaceStateChanged?.Invoke(isFurnaceOn);
    }

    private void SmeltOre()
    {
        if (oreData.Amount < 1)
        {
            oreData.Add(-oreData.Amount);
            outputData.Add(oreData.Amount);
            ChangeFurnaceState(false);
            return;
        }
        oreData.Add(-furnaceData.GetSmeltingRate());
        outputData.Add(furnaceData.GetSmeltingRate());
    }
}
