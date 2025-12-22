using System;

[Serializable]
public class SaveData
{
    public float GoldOreCount;
    public float CurrentMoney;
    public float PlayerMinerLevel;
    public float PlayerMinerUpgradeCurrentCost;
    public float AutomationMinerLevel;
    public float AutomationMinerUpgradeCurrentCost;

    public SaveData()
    {
        GoldOreCount = 0;
        CurrentMoney = 0;
        PlayerMinerLevel = 1;
        PlayerMinerUpgradeCurrentCost = 10;
        AutomationMinerLevel = 0;
        AutomationMinerUpgradeCurrentCost = 500;
    }
}
