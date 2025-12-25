using UnityEngine;

[CreateAssetMenu(fileName = "MinerData", menuName = "Scriptable Objects/MinerData")]
public class MinerData : ScriptableObject, ISaveable
{
    [SerializeField] private string saveID;
    [SerializeField] private float level;
    [SerializeField] private float miningRate = 1f;

    [SerializeField] private bool isAutomation = false;
    
    private float _miningRateMultiplier = 1.5f;

    public string GetSaveID()
    {
        return saveID;
    }

    public string GetSaveData()
    {
        return level.ToString();
    }

    public void LoadFromSaveData(string savedData)
    {
        Debug.Log(savedData);
        if (float.TryParse(savedData, out float savedLevel))
        {
            level = savedLevel;
            UpdateMiningRate();
        }
    }

    [SerializeField] private float defaultLevel = 1f;

    public void ResetData()
    {
        level = defaultLevel;
        UpdateMiningRate();
    }
    
    public void UpdateLevel(float newLevel)
    {
        level += newLevel;
        UpdateMiningRate();
    }

    private void UpdateMiningRate()
    {
        miningRate = level * _miningRateMultiplier;
    }

    public float GetLevel()
    {
        return level;
    }
    
    public float GetMiningRate()
    {
        return miningRate;
    }

    public bool IsAutomation()
    {
        return isAutomation;
    }

}
