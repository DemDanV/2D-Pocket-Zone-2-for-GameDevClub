using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    public static bool CanLoad
    {
        get
        {
            DirectoryInfo directory = new DirectoryInfo(SAVE_FOLDER);
            FileInfo[] saveFile = directory.GetFiles("*.txt");
            return saveFile.Length > 0;
        }
    }

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string saveString)
    {
        int saveNumer = 1;
        while(File.Exists("save_" + saveNumer + ".txt"))
        {
            saveNumer++;
        }
        File.WriteAllText(SAVE_FOLDER + "save_" + saveNumer + ".txt", saveString);
    }

    public static string Load()
    {
        DirectoryInfo directory = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFile = directory.GetFiles("*.txt");
        FileInfo mostRecentFile = null;
        foreach (FileInfo file in saveFile)
        {
            if(mostRecentFile == null)
                mostRecentFile = file;
            else if (file.LastWriteTime > mostRecentFile.LastWriteTime)
                mostRecentFile = file;

        }

        if (mostRecentFile != null)
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + mostRecentFile.Name);
            return saveString;
        }
        else
            return null;
    }
}
