using UnityEngine;

public class MiningController : MonoBehaviour
{
    [SerializeField] private ResourceDataSO oreData;
    [SerializeField] private MinerData minerData;

    private float _timer;
    
    public void ExtractOre()
    {
        oreData.Add(minerData.GetMiningRate());
    }

    private void Update()
    {
        if (minerData.IsAutomation() && minerData.GetLevel() >= 1)
        {
            _timer += Time.deltaTime;

            if (_timer >= 0.5)
            {
                ExtractOre();
                _timer = 0;
            }
        }
    }
}
