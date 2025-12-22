using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [SerializeField] private ResourceDataSO GoldOreData;
    [SerializeField] private ResourceDataSO MoneyData;
    [SerializeField] private MinerData PlayerMinerData;
    [SerializeField] private UpgradeManager PlayerMinerUpgradeManager;
    [SerializeField] private MinerData AutomationMinerData;
    [SerializeField] private UpgradeManager AutomationUpgradeManager;
    
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
        data.PlayerMinerLevel = PlayerMinerData.level;
        data.PlayerMinerUpgradeCurrentCost = PlayerMinerUpgradeManager.GetCurrentCost();
        data.AutomationMinerLevel = AutomationMinerData.level;
        data.AutomationMinerUpgradeCurrentCost = AutomationUpgradeManager.GetCurrentCost();
        
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
            PlayerMinerData.Load(data.PlayerMinerLevel);
            PlayerMinerUpgradeManager.Load(data.PlayerMinerUpgradeCurrentCost);
            AutomationMinerData.Load(data.AutomationMinerLevel);
            AutomationUpgradeManager.Load(data.AutomationMinerUpgradeCurrentCost);
            
            Debug.Log("Veriler Yüklendi.");
        }
        else
        {
            Debug.Log("Kayıt dosyası bulunamadı, yeni oyun başlatılıyor.");
            
            MoneyData.Load(0);
            GoldOreData.Load(0);
            PlayerMinerData.Load(1);
            PlayerMinerUpgradeManager.Load(10);
            AutomationMinerData.Load(0);
            AutomationUpgradeManager.Load(500);
        }
    }

    public void DeleteSave()
    {
        MoneyData.Load(0);
        GoldOreData.Load(0);
        PlayerMinerData.Load(1);
        PlayerMinerUpgradeManager.Load(10);
        AutomationMinerData.Load(0);
        AutomationUpgradeManager.Load(500);
        
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(path))
        {
            File.Delete(path);
            
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
