public interface ISaveable
{
    string GetSaveID();
    string GetSaveData();
    void LoadFromSaveData(string savedData);
    void ResetData();
}
