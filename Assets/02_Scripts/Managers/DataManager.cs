using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager dataInst;
    public GameData gameData = new GameData();
    private string path;
    private string fileName = "GameData";
    private void Awake()
    {
        if (dataInst == null)
        {
            dataInst = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (dataInst != this)
        {
            Destroy(gameObject);
        }
        path = Path.Combine(Application.persistentDataPath, fileName);
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(gameData);
        File.WriteAllText(path, data);
    }
    public void LoadData()
    {
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            gameData = JsonUtility.FromJson<GameData>(data);
        }
    }
}
public class GameData
{
    public Transform playeTr;
    public int scenIdx;
}
