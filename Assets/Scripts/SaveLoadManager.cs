using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }
    private static string saveFilePath;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveGame(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game Saved: " + saveFilePath);
    }
    public PlayerData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Game Loaded");
            return playerData;
        }
        else
        {
            Debug.LogError("Save file not found!");
            return null;
        }
    }
    public bool SaveFileExists()
    {
        return File.Exists(saveFilePath);
    }
}
