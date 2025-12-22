using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "OreDataSO", menuName = "Scriptable Objects/OreDataSO")]
public class ResourceDataSO : ScriptableObject
{
    public float Amount { get; private set; }
    
    public UnityAction<float> OnValueChanged;

    private void OnEnable()
    {
        OnValueChanged = null;
    }

    public void Add(float amount)
    {
        Amount += amount;
        
        OnValueChanged?.Invoke(Amount);
    }

    public void ResetAmount()
    {
        Amount = 0;
        
        OnValueChanged?.Invoke(Amount);
    }
    
    public void Load(float savedAmount)
    {
        Amount = savedAmount;
        OnValueChanged?.Invoke(Amount);
    }
}
