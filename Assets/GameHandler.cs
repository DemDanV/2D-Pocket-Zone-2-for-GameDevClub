using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private void Awake()
    {
        SaveSystem.Init();
    }

    //Called from EscapeMenu-> SaveButton
    public void Save()
    {
        SerializableGameSave gameSave = new SerializableGameSave();
        SaveLoadManager saveLoadManager = GetComponent<SaveLoadManager>();
        if (saveLoadManager != null)
        {
            gameSave.DroppedItems = saveLoadManager.Save();
        }

        string json = JsonUtility.ToJson(gameSave);
        SaveSystem.Save(json);
        PlayerNotificationsManager.singleton.Notify("Saved!");
    }

    public void Load()
    {
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            SerializableGameSave gameSave = JsonUtility.FromJson<SerializableGameSave>(saveString);
            SaveLoadManager saveLoadManager = GetComponent<SaveLoadManager>();
            if (saveLoadManager != null)
                saveLoadManager.Load(gameSave.DroppedItems);
        }
        else
            PlayerNotificationsManager.singleton.Notify("No save");
    }
}

[Serializable]
public class SerializableGameSave
{
    public string DroppedItems;
    public string Player;
    public string Entities;
}
