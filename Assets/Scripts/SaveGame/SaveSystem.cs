using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private const string FILE_NAME = "savegame.json";

    private static string SavePath =>
        Path.Combine(Application.persistentDataPath, FILE_NAME);

    public static void SaveGame(SaveData data)
    {
        if (data == null)
        {
            Debug.LogError("SaveData is NULL!");
            return;
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);

        Debug.Log($"[SAVE] Game saved at: {SavePath}");
    }

    public static SaveData LoadGame()
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("[LOAD] No save file found");
            return null;
        }

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        Debug.Log("[LOAD] Game loaded");
        return data;
    }

    public static bool HasSave()
    {
        return File.Exists(SavePath);
    }
}
