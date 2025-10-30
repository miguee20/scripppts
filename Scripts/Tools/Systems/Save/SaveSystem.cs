using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{ // TODO: refactor
    private static string _path = Application.persistentDataPath + "/save.lrn";
    private static string _settingsPath = Application.persistentDataPath + "/stt-save.lrn";

    public static void Save(int score)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(_path, FileMode.Create);
        SaveData data = new SaveData(score);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveSettings(float bgmVolume, float sfxVolume, bool isLowGraphics)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(_settingsPath, FileMode.Create);
        SettingsSaveData data = new SettingsSaveData(bgmVolume, sfxVolume, isLowGraphics);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData Load()
    {
        if (File.Exists(_path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(_path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + _path);
            return null;
        }
    }

    public static SettingsSaveData LoadSettings()
    {
        if (File.Exists(_settingsPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(_settingsPath, FileMode.Open);
            SettingsSaveData data = formatter.Deserialize(stream) as SettingsSaveData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + _settingsPath);
            return null;
        }
    }
}