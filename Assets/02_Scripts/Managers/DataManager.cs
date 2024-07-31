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
    private string itemDataFileName = "SunDryItemData.json"; // ������ ������ JSON ���� �̸�
    private string questDataFileName = "QuestSaveData.json";
    private string skillDataFileName = "SkillData.json"; // ��ų ������ JSON ���� �̸�
    public List<ItemDataJson> Items = new List<ItemDataJson>();
    public List<QuestDataJson> Quests = new List<QuestDataJson>();
    public List<RectTransform> uiElements; // ������ UI ��� ���
    public Canvas canvas; // �ֻ��� Canvas

    // ���⿡ ������ ������ ������ �߰�

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

            LoadItemData(); // ������ ������ �ҷ�����
            LoadQuest();
            LoadSkillData();
            Debug.Log($"Game data path: {path}");
            Debug.Log($"Item data path: {Path.Combine(Application.persistentDataPath, itemDataFileName)}");
            Debug.Log($"Quest data path: {Path.Combine(Application.persistentDataPath, questDataFileName)}");
        }
    }

    public void SaveItemData(ItemType type, string name, string path, int count)
    {

        // ���ο� ������ ������ ����
        ItemDataJson newItem = new ItemDataJson
        {
            Count = count,
            Name = name,
            Path = path,
            itemType = type
        };

        // ���� �������� �ִ��� Ȯ��
        ItemDataJson existingItem = itemSaveData.Items.Find(item => item.Name == name && item.itemType == type);
        if (existingItem != null)
        {
            // ���� �������� �ִٸ� ������ ������Ʈ
            existingItem.Count = count;
            existingItem.Path = path;
        }
        else
        {
            // ���� �������� ���ٸ� ���ο� ������ �߰�
            itemSaveData.Items.Add(newItem);
        }

        // JSON���� ��ȯ�Ͽ� ���Ͽ� ����
        string json = JsonUtility.ToJson(itemSaveData, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, itemDataFileName), json);
    }

    public void SaveQuestData(QuestState type, string name_, int killCount = 0, int bossKillCount = 0)
    {
        // ���ο� ����Ʈ ������ ����
        QuestDataJson newQuest = new QuestDataJson
        {
            Name = name_,
            KillCount = killCount,
            bossKillCount = bossKillCount,
            questState = type
        };

        // ���� ����Ʈ�� �ִ��� Ȯ��
        QuestDataJson existingQuest = questSaveData.Quests.Find(quest => quest.Name == name_);

        if (existingQuest != null)
        {
            // ���� ����Ʈ�� �ִٸ� ���¿� ī��Ʈ�� ������Ʈ
            existingQuest.KillCount = killCount;
            existingQuest.bossKillCount = bossKillCount;
            existingQuest.questState = type;
        }
        else
        {
            // ���� ����Ʈ�� ���ٸ� ���ο� ����Ʈ �߰�
            questSaveData.Quests.Add(newQuest);
        }

        // JSON���� ��ȯ�Ͽ� ���Ͽ� ����
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
        // ���ο� ��ų ������ ����Ʈ ����
        skillDataJson.skillDataList = skillDataList;

        // JSON���� ��ȯ�Ͽ� ���Ͽ� ����
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
    public string Name;       // �������� �̸�
    public int Count;         // �������� ����
    public string Path;       // �������� ��ġ ���
    public ItemType itemType;
}
public enum ItemType { Sword, Shield, Hat, Cloth, Pants, Shoes, Neck, Kloak, Ring, Material, Gold, HPPotion, MPPotion, Nothing }
[System.Serializable]
public class QuestDataJson
{
    public string Name;     //����Ʈ �̸�
    public int KillCount;   //����Ʈ ��ǥ ųī��Ʈ
    public int bossKillCount;
    public QuestState questState;   // ����Ʈ ����
}
public enum QuestState { QuestHave, QuestTake, QuestClear, None, QuestNormal }
public enum SkillState { None, Ice, Fire, Electro }
public enum SkillLevelState { None, Lv05, Lv10, Lv20_01, Lv20_02, Lv30 }
public enum SkillSelecState { None, Q, E, R, F, C }
[System.Serializable] public class ItemSaveData { public List<ItemDataJson> Items = new List<ItemDataJson>(); }
[System.Serializable] public class QuestSaveData { public List<QuestDataJson> Quests = new List<QuestDataJson>(); }
[System.Serializable] public class SkillDataJson { public List<SkillData> skillDataList = new List<SkillData>(); }
