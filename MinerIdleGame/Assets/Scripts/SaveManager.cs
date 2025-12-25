using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    
    [Header("Save Settings")]
    [SerializeField] private string saveFileName = "gamedata.json";
    
    [Header("Data to Save")]
    [Tooltip("Drag all ScriptableObjects that implement ISaveable here.")]
    [SerializeField] private List<ScriptableObject> saveableObjects;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
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

        foreach (var obj in saveableObjects)
        {
            if (obj is ISaveable saveable)
            {
                data.SavedItems.Add(new SaveItem(saveable.GetSaveID(), saveable.GetSaveData()));
            }
        }
        
        string json = JsonUtility.ToJson(data, true); // Pretty print for debug
        
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        File.WriteAllText(path, json);
        
        Debug.Log("Game Saved: " + path);
    }

    public void LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            
            // Convert list to dictionary for faster lookup
            Dictionary<string, string> savedDataMap = new Dictionary<string, string>();
            if (data.SavedItems != null)
            {
                foreach (var item in data.SavedItems)
                {
                    if (!savedDataMap.ContainsKey(item.Key))
                    {
                        savedDataMap.Add(item.Key, item.Value);
                    }
                }
            }

            foreach (var obj in saveableObjects)
            {
                if (obj is ISaveable saveable)
                {
                    string id = saveable.GetSaveID();
                    if (savedDataMap.ContainsKey(id))
                    {
                        saveable.LoadFromSaveData(savedDataMap[id]);
                    }
                    else
                    {
                        // New data type added since last save? Reset it or leave default.
                        // Ideally we reset it if we expect clean state, but usually default values are enough.
                        // Or we can call ResetData() if we want strict enforcement.
                    }
                }
            }
            
            Debug.Log("Data Loaded.");
        }
        else
        {
            Debug.Log("Save file not found, starting a new game.");
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
            Debug.Log("Save Deleted!");
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
        foreach (var obj in saveableObjects)
        {
            if (obj is ISaveable saveable)
            {
                saveable.ResetData();
            }
        }
    }
}
