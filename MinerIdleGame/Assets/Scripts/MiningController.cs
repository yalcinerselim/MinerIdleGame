using UnityEngine;

public class MiningController : MonoBehaviour
{
    [SerializeField] private ResourceDataSO oreData;
    [SerializeField] private float miningRate = 1f;

    public void ExtractOre()
    {
        oreData.Add(miningRate);
    }

    public void UpgradeMiningRate(float amount)
    {
        miningRate += amount;
    }

    public float GetCurrentMiningRate()
    {
        return miningRate;
    }

    public void Load(float savedMiningRate)
    {
        miningRate = savedMiningRate;
    }
}
