using UnityEngine;
using UnityEngine.UI;

public class OreFurnace : MonoBehaviour
{
    [SerializeField] private FurnaceDataSO furnaceData;
    [SerializeField] private Button myButton;
    
    [SerializeField] private ResourceDataSO oreData;
    [SerializeField] private ResourceDataSO outputData;
    [SerializeField] private ResourceDataSO moneyData;
    
    public bool isFurnaceOn = false;
    private float _timer;

    private void Awake()
    {
        myButton = GetComponent<Button>();
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

    public void TurnOnFurnace()
    {
        if (oreData.Amount >= 1)
        {
            isFurnaceOn = true;
        }
    }

    private void SmeltOre()
    {
        if (oreData.Amount < 1)
        {
            isFurnaceOn = false;
            return;
        }
        oreData.Add(-furnaceData.GetSmeltingRate());
        outputData.Add(furnaceData.GetSmeltingRate());
    }
}
