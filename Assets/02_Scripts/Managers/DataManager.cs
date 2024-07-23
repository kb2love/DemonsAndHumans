using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager dataInst;
    public GameData gameData = new GameData();
    private string path;
    private string fileName = "GameData.json";

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
    public void DataSave()
    {
        Transform playerTr = GameObject.Find("Player").transform;
        dataInst.gameData.IsSave = true;
        gameData.sceneIdx = SceneMove.SceneInst.currentScene;
        gameData.playerPosition = new float[] { playerTr.position.x, playerTr.position.y, playerTr.position.z };
        gameData.playerRotation = new float[] { playerTr.rotation.eulerAngles.x, playerTr.rotation.eulerAngles.y, playerTr.rotation.eulerAngles.z };
        SaveData();
    }
}

[System.Serializable]
public class GameData
{
    public float[] playerPosition;
    public float[] playerRotation;
    public int sceneIdx;
    public bool IsSave;
}
