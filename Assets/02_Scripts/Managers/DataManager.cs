using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager dataInst;
    public GameData gameData = new GameData();
    public ItemSaveData itemSaveData = new ItemSaveData();
    public QuestSaveData questSaveData = new QuestSaveData();
    public SkillDataJson skillDataJson = new SkillDataJson();
    private string path;
    private string fileName = "GameData.json";
    private string itemDataFileName = "SunDryItemData.json"; // 아이템 데이터 JSON 파일 이름
    private string questDataFileName = "QuestSaveData.json";
    private string skillDataFileName = "SkillData.json"; // 스킬 데이터 JSON 파일 이름
    public List<ItemDataJson> Items = new List<ItemDataJson>();
    public List<QuestDataJson> Quests = new List<QuestDataJson>();
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

    public void GameDataSave()
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
        string questDataPath = Path.Combine(Application.persistentDataPath, questDataFileName);
        string skillDataPath = Path.Combine(Application.persistentDataPath, skillDataFileName);

        if (File.Exists(gameDataPath)) { File.Delete(gameDataPath); }
        if (File.Exists(itemDataPath)) { File.Delete(itemDataPath); }
        if (File.Exists(questDataPath)) { File.Delete(questDataPath); }
        if (File.Exists(skillDataPath)) { File.Delete(skillDataPath); }
    }

    public void DataSave()
    {
        Transform playerTr = GameObject.Find("Player").transform;
        gameData.IsSave = true;
        gameData.sceneIdx = SceneMove.SceneInst.currentScene;
        gameData.playerPosition = new float[] { playerTr.position.x, playerTr.position.y, playerTr.position.z };
        gameData.playerRotation = new float[] { playerTr.rotation.eulerAngles.x, playerTr.rotation.eulerAngles.y, playerTr.rotation.eulerAngles.z };

        GameDataSave();
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
            LoadQuest();
            LoadSkillData();
            Debug.Log($"Game data path: {path}");
            Debug.Log($"Item data path: {Path.Combine(Application.persistentDataPath, itemDataFileName)}");
            Debug.Log($"Quest data path: {Path.Combine(Application.persistentDataPath, questDataFileName)}");
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

    public void SaveQuestData(QuestState type, string name_, int killCount = 0, int bossKillCount = 0)
    {
        // 새로운 퀘스트 데이터 생성
        QuestDataJson newQuest = new QuestDataJson
        {
            Name = name_,
            KillCount = killCount,
            bossKillCount = bossKillCount,
            questState = type
        };

        // 기존 퀘스트가 있는지 확인
        QuestDataJson existingQuest = questSaveData.Quests.Find(quest => quest.Name == name_);

        if (existingQuest != null)
        {
            // 기존 퀘스트가 있다면 상태와 카운트를 업데이트
            existingQuest.KillCount = killCount;
            existingQuest.bossKillCount = bossKillCount;
            existingQuest.questState = type;
        }
        else
        {
            // 기존 퀘스트가 없다면 새로운 퀘스트 추가
            questSaveData.Quests.Add(newQuest);
        }

        // JSON으로 변환하여 파일에 저장
        string json = JsonUtility.ToJson(questSaveData, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, questDataFileName), json);
    }
    public QuestSaveData LoadQuest()
    {
        string jsonPath = Path.Combine(Application.persistentDataPath, questDataFileName);
        if (File.Exists(jsonPath))
        {
            string json = File.ReadAllText(jsonPath);
            questSaveData = JsonUtility.FromJson<QuestSaveData>(json);
            Quests = questSaveData.Quests; // Load the items into the Items list
        }
        return questSaveData;
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
    public void SaveSkillData(List<SkillData> skillDataList)
    {
        // 새로운 스킬 데이터 리스트 생성
        skillDataJson.skillDataList = skillDataList;

        // JSON으로 변환하여 파일에 저장
        string json = JsonUtility.ToJson(skillDataJson, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, skillDataFileName), json);
    }
    public SkillDataJson LoadSkillData()
    {
        string jsonPath = Path.Combine(Application.persistentDataPath, skillDataFileName);
        if (File.Exists(jsonPath))
        {
            string json = File.ReadAllText(jsonPath);
            skillDataJson = JsonUtility.FromJson<SkillDataJson>(json);
        }
        return null;
    }
    public SkillData GetSkillDataByName(string dataName, SkillData defaultSkillData)
    {
        var foundSkillData = skillDataJson.skillDataList.Find(skillData => skillData.DataName == dataName);
        return foundSkillData ?? defaultSkillData;
    }

    public ItemDataJson FindItem(ItemType itemType, string itemName) { var foundItem = Items.Find(item => item.itemType == itemType && item.Name == itemName); return foundItem; }
    public QuestDataJson FindQuest(QuestState state, string questName) { var foundQuest = Quests.Find(quest => quest.questState == state && quest.Name == questName); return foundQuest; }
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
public enum ItemType { Sword, Shield, Hat, Cloth, Pants, Shoes, Neck, Kloak, Ring, Material, Gold, HPPotion, MPPotion, Nothing }
[System.Serializable]
public class QuestDataJson
{
    public string Name;     //퀘스트 이름
    public int KillCount;   //퀘스트 목표 킬카운트
    public int bossKillCount;
    public QuestState questState;   // 퀘스트 상태
}
public enum QuestState { QuestHave, QuestTake, QuestClear, None, QuestNormal }
public enum SkillState { None, Ice, Fire, Electro }
public enum SkillLevelState { None, Lv05, Lv10, Lv20_01, Lv20_02, Lv30 }
public enum SkillSelecState { None, Q, E, R, F, C }
[System.Serializable] public class ItemSaveData { public List<ItemDataJson> Items = new List<ItemDataJson>(); }
[System.Serializable] public class QuestSaveData { public List<QuestDataJson> Quests = new List<QuestDataJson>(); }
[System.Serializable] public class SkillDataJson { public List<SkillData> skillDataList = new List<SkillData>(); }
