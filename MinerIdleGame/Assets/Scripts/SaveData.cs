using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public List<SaveItem> SavedItems;

    public SaveData()
    {
        SavedItems = new List<SaveItem>();
    }
}

[Serializable]
public struct SaveItem
{
    public string Key;
    public string Value;

    public SaveItem(string key, string value)
    {
        Key = key;
        Value = value;
    }
}
