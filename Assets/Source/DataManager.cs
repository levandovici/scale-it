using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class DataManager
{
    public static Data Current = new Data();

    private static bool Initialized = false;



    public static void Initialize()
    {
        if (Initialized)
            return;

        Initialized = true;

        Load();
    }



    public static void Load()
    {
        string path;

#if UNITY_ANDROID && !UNITY_EDITOR
        
        path = Application.persistentDataPath;

#else

        path = Application.dataPath;

#endif

        try
        {
            Current = JsonUtility.FromJson<Data>(File.ReadAllText(Path.Combine(path, "data.json")));
        }
        catch
        {
            Current = new Data();
        }
    }

    public static void Save()
    {
        string path;

#if UNITY_ANDROID && !UNITY_EDITOR
        
        path = Application.persistentDataPath;

#else

        path = Application.dataPath;

#endif

        try
        {
            File.WriteAllText(Path.Combine(path, "data.json"), JsonUtility.ToJson(Current));
        }
        catch
        {

        }
    }
}

[System.Serializable]
public class Data
{
    public int best_score = 0;



    public Data()
    {
        best_score = 0;
    }
}