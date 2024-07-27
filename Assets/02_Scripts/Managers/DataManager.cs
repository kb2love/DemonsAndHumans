using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEditor.Progress;

public class DataManager : MonoBehaviour
{
    public static DataManager dataInst;
    public GameData gameData = new GameData();
    public ItemSaveData itemSaveData = new ItemSaveData();
    private string path;
    private string fileName = "GameData.json";
    private string itemDataFileName = "ItemData.json"; // 아이템 데이터 JSON 파일 이름
    public List<ItemDataJson> Items = new List<ItemDataJson>();

    public List<RectTransform> uiElements; // 저장할 UI 요소 목록
    public Canvas canvas; // 최상위 Canvas

    // 여기에 나머지 아이템 데이터 추가

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
        LoadData();
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(gameData);
        Debug.Log($"Saving data to path: {path}");
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
    public bool FileExistst()
    {
        if (File.Exists(path)) return true;
        else return false;
    }
    public void DeleteSaveData()
    {
        string gameDataPath = Path.Combine(Application.persistentDataPath, fileName);
        string itemDataPath = Path.Combine(Application.persistentDataPath, itemDataFileName);

        if (File.Exists(gameDataPath))
        {
            File.Delete(gameDataPath);
        }

        if (File.Exists(itemDataPath))
        {
            File.Delete(itemDataPath);
        }
    }
    public void DataSave()
    {
        Transform playerTr = GameObject.Find("Player").transform;
        gameData.IsSave = true;
        gameData.sceneIdx = SceneMove.SceneInst.currentScene;
        gameData.playerPosition = new float[] { playerTr.position.x, playerTr.position.y, playerTr.position.z };
        gameData.playerRotation = new float[] { playerTr.rotation.eulerAngles.x, playerTr.rotation.eulerAngles.y, playerTr.rotation.eulerAngles.z };

        SaveData();
    }

    public void DataLoad()
    {
        LoadData();
        if (gameData.IsSave)
        {
            Transform playerTr = GameObject.Find("Player").transform;
            playerTr.position = new Vector3(gameData.playerPosition[0], gameData.playerPosition[1], gameData.playerPosition[2]);
            playerTr.rotation = Quaternion.Euler(gameData.playerRotation[0], gameData.playerRotation[1], gameData.playerRotation[2]);

            LoadItemData(); // 아이템 데이터 불러오기
        }
    }

    public void SaveItemData(ItemType type, string name, string path, int count)
    {

        // 새로운 아이템 데이터 생성
        ItemDataJson newItem = new ItemDataJson
        {
            Count = count,
            Name = name,
            Path = path,
            itemType = type
        };

        // 기존 아이템이 있는지 확인
        ItemDataJson existingItem = itemSaveData.Items.Find(item => item.Name == name && item.itemType == type);
        if (existingItem != null)
        {
            // 기존 아이템이 있다면 수량을 업데이트
            existingItem.Count = count;
            existingItem.Path = path;
        }
        else
        {
            // 기존 아이템이 없다면 새로운 아이템 추가
            itemSaveData.Items.Add(newItem);
        }

        // JSON으로 변환하여 파일에 저장
        string json = JsonUtility.ToJson(itemSaveData, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, itemDataFileName), json);
    }

    public ItemSaveData LoadItemData()
    {
        string jsonPath = Path.Combine(Application.persistentDataPath, itemDataFileName);
        if (File.Exists(jsonPath))
        {
            string json = File.ReadAllText(jsonPath);
            itemSaveData = JsonUtility.FromJson<ItemSaveData>(json);
            Items = itemSaveData.Items; // Load the items into the Items list
        }
        return itemSaveData;
    }
    public ItemDataJson FindItem(ItemType itemType, string itemName)
    {
        var foundItem = Items.Find(item => item.itemType == itemType && item.Name == itemName);
        return foundItem;
    }
}

    [System.Serializable]
public class GameData
{
    public float[] playerPosition;
    public float[] playerRotation;
    public int sceneIdx;
    public bool IsSave = false;
}

[System.Serializable]
public class ItemDataJson
{
    public string Name;       // 아이템의 이름
    public int Count;         // 아이템의 개수
    public string Path;       // 아이템의 위치 경로
    public ItemType itemType;
}
public enum ItemType
{
    Sword, Shield, Hat, Cloth, Pants, Shoes, Neck, Kloak, Ring, Material, Gold, HPPotion, MPPotion, Nothing
}
[System.Serializable]
public class QuestDataJson
{
    public string Name;
    public int KillCount;
    public int CluearCOunt;
    public QuestState questState;
}
public enum QuestState { QuestHave, QuestTake, QuestClear, None }
[System.Serializable]
public class ItemSaveData
{
    public List<ItemDataJson> Items = new List<ItemDataJson>();
}