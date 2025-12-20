using UnityEngine;

public class MiningController : MonoBehaviour
{
    [SerializeField] private ResourceDataSO oreData;
    [SerializeField] private ResourceDataSO moneyData;
    [SerializeField] private float miningRate = 1f;
    
    public void ExtractOre()
    {
        Debug.Log("Extracting ore");
        oreData.Add(miningRate);
    }
}
