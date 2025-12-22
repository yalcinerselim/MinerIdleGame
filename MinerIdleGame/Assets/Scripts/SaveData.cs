using System;

[Serializable]
public class SaveData
{
    public float GoldOreCount;
    public float CurrentMoney;
    public float MiningRate;
    public float CurrentCost;

    public SaveData()
    {
        GoldOreCount = 0;
        CurrentMoney = 0;
        MiningRate = 1;
        CurrentCost = 10;
    }
}
