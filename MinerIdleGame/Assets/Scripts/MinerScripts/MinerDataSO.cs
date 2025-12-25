using UnityEngine;

[CreateAssetMenu(fileName = "MinerData", menuName = "Scriptable Objects/MinerData")]
public class MinerData : ScriptableObject
{
    [SerializeField] private float level;
    [SerializeField] private float miningRate = 1f;

    [SerializeField] private bool isAutomation = false;
    
    private float _miningRateMultiplier = 1.5f;

    public void Load(float savedLevel)
    {
        level = savedLevel;
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
