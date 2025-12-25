using UnityEngine;

public class ResourceSeller : MonoBehaviour
{
    [SerializeField] private ResourceDataSO resourceToSell;
    [SerializeField] private ResourceDataSO moneyData;

    [SerializeField] private float pricePerUnit;
    
    public void SellAllResources()
    {
        if (resourceToSell.Amount < 0) return;
        
        float totalEarnings = resourceToSell.Amount * pricePerUnit;
        
        moneyData.Add(totalEarnings);
        
        resourceToSell.ResetAmount();
    }
}
