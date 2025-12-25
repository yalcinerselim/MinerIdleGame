using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    
    
    [SerializeField] private ResourceDataSO GoldOreData;
    [SerializeField] private ResourceDataSO GoldData;
    [SerializeField] private ResourceDataSO MoneyData;
    [SerializeField] private MinerData PlayerMinerData;
    [SerializeField] private MinerUpgradeDataSO PlayerMinerUpgradeData;
    [SerializeField] private MinerData AutomationMinerData;
    [SerializeField] private MinerUpgradeDataSO AutomationUpgradeData;
    
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
        data.GoldCount= GoldData.Amount;
        data.PlayerMinerLevel = PlayerMinerData.GetLevel();
        data.PlayerMinerUpgradeCurrentCost = PlayerMinerUpgradeData.GetCurrentCost();
        data.AutomationMinerLevel = AutomationMinerData.GetLevel();
        data.AutomationMinerUpgradeCurrentCost = AutomationUpgradeData.GetCurrentCost();
        
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
            GoldData.Load(data.GoldCount);
            PlayerMinerData.Load(data.PlayerMinerLevel);
            PlayerMinerUpgradeData.Load(data.PlayerMinerUpgradeCurrentCost);
            AutomationMinerData.Load(data.AutomationMinerLevel);
            AutomationUpgradeData.Load(data.AutomationMinerUpgradeCurrentCost);
            
            Debug.Log("Veriler Yüklendi.");
        }
        else
        {
            Debug.Log("Kayıt dosyası bulunamadı, yeni oyun başlatılıyor.");
            
            ResetGame();
        }
    }

    public void DeleteSave()
    {
        ResetGame();
        
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

    private void ResetGame()
    {
        MoneyData.Load(0);
        GoldOreData.Load(0);
        GoldData.Load(0);
        PlayerMinerData.Load(1);
        PlayerMinerUpgradeData.Load(10);
        AutomationMinerData.Load(0);
        AutomationUpgradeData.Load(500);
    }
}
