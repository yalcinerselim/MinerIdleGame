using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [SerializeField] private ResourceDataSO GoldOreData;
    [SerializeField] private ResourceDataSO MoneyData;
    [SerializeField] private MiningController MiningController;
    [SerializeField] private UpgradeManager UpgradeManager;
    
    private string saveFileName = "gamedata.json";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.CurrentMoney = MoneyData.Amount;
        data.GoldOreCount = GoldOreData.Amount;
        data.MiningRate = MiningController.GetCurrentMiningRate();
        data.CurrentCost = UpgradeManager.GetCurrentCost();
        
        string json = JsonUtility.ToJson(data);
        
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        File.WriteAllText(path, json);
        
        Debug.Log("Oyun Kaydedildi: " + path);
    }

    public void LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            
            MoneyData.Load(data.CurrentMoney);
            GoldOreData.Load(data.GoldOreCount);
            MiningController.Load(data.MiningRate);
            UpgradeManager.Load(data.CurrentCost);
            
            Debug.Log("Veriler Yüklendi.");
        }
        else
        {
            Debug.Log("Kayıt dosyası bulunamadı, yeni oyun başlatılıyor.");
            
            MoneyData.Load(0);
            GoldOreData.Load(0);
            MiningController.Load(1);
            UpgradeManager.Load(10);
        }
    }

    public void DeleteSave()
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(path))
        {
            File.Delete(path);
            
            MoneyData.Load(0);
            GoldOreData.Load(0);
            MiningController.Load(1);
            UpgradeManager.Load(10);
            
            Debug.Log("Kayıt Silindi!");
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
