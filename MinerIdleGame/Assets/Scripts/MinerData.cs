using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MinerData", menuName = "Scriptable Objects/MinerData")]
public class MinerData : ScriptableObject
{
    [SerializeField] public float level = 0;
    [SerializeField] public float miningRate = 1.5f;

    [SerializeField] public bool isAutomation = true;

    public void Load(float savedLevel)
    {
        level = savedLevel;
        miningRate = level * 1.5f;
    }
    
    public void UpgradeMiningRate()
    {
        level += 1;
        miningRate = level * 1.5f;
    }
    
}
