using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "OreDataSO", menuName = "Scriptable Objects/OreDataSO")]
public class ResourceDataSO : ScriptableObject, ISaveable
{
    [SerializeField] private string saveID;
    public float Amount { get; private set; }
    
    public UnityAction<float> OnValueChanged;

    private void OnEnable()
    {
        OnValueChanged = null;
    }

    public string GetSaveID()
    {
        return saveID;
    }

    public string GetSaveData()
    {
        return Amount.ToString();
    }

    public void LoadFromSaveData(string savedData)
    {
        if (float.TryParse(savedData, out float savedAmount))
        {
            Amount = savedAmount;
            OnValueChanged?.Invoke(Amount);
        }
    }

    [SerializeField] private float initialAmount = 0;

    public void ResetData()
    {
        Amount = initialAmount;
        OnValueChanged?.Invoke(Amount);
    }
    
    public void Add(float amount)
    {
        Amount += amount;
        
        OnValueChanged?.Invoke(Amount);
    }

    public void ResetAmount()
    {
        ResetData();
    }
    
    public void Load(float savedAmount)
    {
        Amount = savedAmount;
        OnValueChanged?.Invoke(Amount);
    }
}
