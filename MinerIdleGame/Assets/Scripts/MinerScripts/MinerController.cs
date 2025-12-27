using UnityEngine;
using PrimeTween;

public class MinerController : MonoBehaviour
{
    [SerializeField] private ResourceDataSO oreData;
    [SerializeField] private MinerData minerData;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float scaleMultiplier = 1.1f;
    [SerializeField] private Transform miningButton;

    private float _timer;
    private Vector3 _initialScale;
    private Tween _scaleTween;

    private void Awake()
    {
        if (miningButton != null)
        {
            _initialScale = miningButton.localScale;
        }
    }
    
    public void ExtractOre()
    {
        oreData.Add(minerData.GetMiningRate());
        if (miningButton != null)
        {
            if (_scaleTween.isAlive)
            {
                _scaleTween.Stop();
                miningButton.localScale = _initialScale;
            }
            
            _scaleTween = Tween.Scale(miningButton, _initialScale * scaleMultiplier, animationDuration, Ease.OutQuad, 2, CycleMode.Yoyo);
        }
    }

    private void Update()
    {
        if (minerData.IsAutomation() && minerData.GetLevel() >= 1)
        {
            _timer += Time.deltaTime;

            if (_timer >= 1)
            {
                ExtractOre();
                _timer = 0;
            }
        }
    }
}
