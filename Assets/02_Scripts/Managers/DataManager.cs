using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager dataInst;
    public GameData gameData = new GameData();
    public ItemSaveData itemSaveData = new ItemSaveData();
    public QuestSaveData questSaveData = new QuestSaveData();
    public SkillSaveData skillSaveData = new SkillSaveData();
    public PlayerDataJson playerDataJson = new PlayerDataJson();
    private string path;
    private string fileName = "GameData.json";
    private string itemDataFileName = "ItemData.json"; // ������ ������ JSON ���� �̸�
    private string questDataFileName = "QuestSaveData.json";
    private string skillDataFileName = "SkillData.json"; // ��ų ������ JSON ���� �̸�
    private string playerDataFileName = "PlayerData.json";
    public List<ItemDataJson> Items = new List<ItemDataJson>();
    public List<QuestDataJson> Quests = new List<QuestDataJson>();
    public List<SkillDataJson> Skills = new List<SkillDataJson>();
    public List<RectTransform> uiElements; // ������ UI ��� ���
    public Canvas canvas; // �ֻ��� Canvas
    // ����Ʈ �����͵�
    public QuestDataJson paladinQuestDataJson = new QuestDataJson();
    public QuestDataJson leaderQuestDataJson = new QuestDataJson();
    public QuestDataJson mariaQuest_01DataJson = new QuestDataJson();
    public QuestDataJson mariaQuest_02DataJson = new QuestDataJson();
    public QuestDataJson mutantKillerQuest_01DataJson = new QuestDataJson();
    public QuestDataJson mutantKillerQuest_02DataJson = new QuestDataJson();
    public QuestDataJson mutantKillerQuest_03DataJson = new QuestDataJson();
    public QuestDataJson mutantKillerQuest_04DataJson = new QuestDataJson();


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
        string playerDataPaht = Path.Combine(Application.persistentDataPath, playerDataFileName);
        if (File.Exists(gameDataPath)) { File.Delete(gameDataPath); }
        if (File.Exists(itemDataPath)) { File.Delete(itemDataPath); }
        if (File.Exists(questDataPath)) { File.Delete(questDataPath); }
        if (File.Exists(skillDataPath)) { File.Delete(skillDataPath); }
        if (File.Exists(playerDataPaht)) { File.Delete(playerDataPaht); }
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
            PlayerDataLoad();
            Debug.Log($"Game data path: {path}");
            Debug.Log($"Item data path: {Path.Combine(Application.persistentDataPath, itemDataFileName)}");
            Debug.Log($"Quest data path: {Path.Combine(Application.persistentDataPath, questDataFileName)}");
            leaderQuestDataJson = FindQuest("�������� ���");
            paladinQuestDataJson = FindQuest("��������");
            mariaQuest_01DataJson = FindQuest("���ܿ� �׽�Ʈ");
            mariaQuest_02DataJson = FindQuest("������ɴ��");
            mutantKillerQuest_01DataJson = FindQuest("������ɴ� �׽�Ʈ");
            mutantKillerQuest_02DataJson = FindQuest("�ϱ� ���� ���� ������");
            mutantKillerQuest_03DataJson = FindQuest("������ �ĸ��� �ڸ���");
            mutantKillerQuest_04DataJson = FindQuest("������ �ƽ��� ���콺");
        }
    }
    #region �÷��̾� ������ ���� / �ε�

    public void PlayerDataSave(PlayerDataJson data)
    {
        // PlayerDataJson ��ü�� JSON ���ڿ��� ��ȯ
        string json = JsonUtility.ToJson(data, true);

        // JSON ���ڿ��� ���Ͽ� ����
        string filePath = Path.Combine(Application.persistentDataPath, playerDataFileName);
        File.WriteAllText(filePath, json);
    }

    // �÷��̾� �����͸� �ε��ϴ� �޼���
    public PlayerDataJson PlayerDataLoad()
    {
        string filePath = Path.Combine(Application.persistentDataPath, playerDataFileName);

        // ������ �����ϴ��� Ȯ��
        if (File.Exists(filePath))
        {
            // ���Ͽ��� JSON ���ڿ��� �о��
            string json = File.ReadAllText(filePath);

            // JSON ���ڿ��� PlayerDataJson ��ü�� ��ȯ
            playerDataJson = JsonUtility.FromJson<PlayerDataJson>(json);
        }
        else
        {
            // ������ �������� ������ ���ο� PlayerDataJson ��ü�� ����
            playerDataJson = new PlayerDataJson();
        }

        return playerDataJson;
    }

    #endregion
    #region ������ ����/�ε�
    public void SaveItemData(string name, string path, int count, int idx)
    {

        // ���ο� ������ ������ ����
        ItemDataJson newItem = new ItemDataJson
        {
            Count = count,
            Name = name,
            Path = path,
            Idx = idx
        };

        // ���� �������� �ִ��� Ȯ��
        ItemDataJson existingItem = itemSaveData.Items.Find(item => item.Name == name);
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
    public ItemDataJson FindItem(string itemName) { var foundItem = Items.Find(item => item.Name == itemName); return foundItem ?? new ItemDataJson(); }
    #endregion
    #region ����Ʈ ����/�ε�
    public void SaveQuestData(QuestState type, string name_, int questIdx, int killCount = 0, int bossKillCount = 0)
    {
        // ���ο� ����Ʈ ������ ����
        QuestDataJson newQuest = new QuestDataJson
        {
            Name = name_,
            KillCount = killCount,
            bossKillCount = bossKillCount,
            Idx = questIdx,
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
            Debug.Log($"Loaded Quest Data: {json}");
        }
        else
        {
            Debug.LogWarning("Quest data file not found.");
        }
        return questSaveData;
    }

    public QuestDataJson FindQuest(string questName)
    {
        var foundQuest = Quests.Find(quest => quest.Name == questName);
        return foundQuest ?? new QuestDataJson(); // �����Ͱ� ������ ���ο� QuestDataJson�� ��ȯ
    }
    #endregion

    #region ��ų ���� / �ε�
    public void SaveSkillData(string name_, SkillState skillState, SkillSelecState skillSelecState)
    {
        // ���� ��ų�� �ִ��� Ȯ��
        SkillDataJson newSkill = new SkillDataJson
        {
            DataName = name_,
            SkillState = skillState,
            SkillSelecState = skillSelecState
        };
        SkillDataJson existingSkill = skillSaveData.Skills.Find(skill => skill.DataName == name_);

        if (existingSkill != null)
        {
            // ���� ��ų�� �ִٸ� ������Ʈ
            existingSkill.DataName = name_;
            existingSkill.SkillState = skillState;
            existingSkill.SkillSelecState = skillSelecState;
        }
        else
        {
            // ���� ��ų�� ���ٸ� ���ο� ��ų �߰�
            skillSaveData.Skills.Add(newSkill);
        }

        // JSON���� ��ȯ�Ͽ� ���Ͽ� ����
        string json = JsonUtility.ToJson(skillSaveData, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, skillDataFileName), json);
    }

    public SkillSaveData LoadSkillData()
    {
        string jsonPath = Path.Combine(Application.persistentDataPath, skillDataFileName);
        if (File.Exists(jsonPath))
        {
            string json = File.ReadAllText(jsonPath);
            skillSaveData = JsonUtility.FromJson<SkillSaveData>(json);
            Skills = skillSaveData.Skills;
        }
        else
        {
            Debug.LogWarning("Skill data file not found.");
        }
        return skillSaveData;
    }

    public SkillDataJson FindSkill(string skillName)
    {
        var foundSkill = Skills.Find(skill => skill.DataName == skillName);
        return foundSkill ?? new SkillDataJson(); // �����Ͱ� ������ ���ο� SkillDataJson ��ȯ
    }
    #endregion
}
[System.Serializable]
public class GameData
{
    public float[] playerPosition;  //�÷��̾� ��ġ
    public float[] playerRotation;  //�÷��̾� Rot
    public int sceneIdx;            //�÷��̾ ���� �ִ� SceneIdx
    public bool IsSave = false;     // ������ ������ ���ߴ���
}
[System.Serializable]
public class ItemDataJson
{
    public string Name;       // �������� �̸�
    public int Count;         // �������� ����
    public int Idx;
    public string Path;       // �������� ��ġ ���
}
[System.Serializable]
public class QuestDataJson
{
    public string Name;     //����Ʈ �̸�
    public int Idx;
    public int KillCount;   //����Ʈ ��ǥ ųī��Ʈ
    public int bossKillCount;
    public QuestState questState;   // ����Ʈ ����
}
[System.Serializable]
public class PlayerDataJson
{
    public float HP;                //ü��
    public float MaxHP;             //�ִ�ü��
    public float MP;                //����
    public float MaxMP;             //�ִ븶��
    public float AttackValue;            //������
    public float MagicAttackValue;
    public float expValue;        //����ġ
    public float maxExpValue;     //�ִ����ġ
    public int Level;             //����
    public float DefenceValue;      //����
    public float FatalProbability;  //ġ��ŸȮ��
    public float FatalAttackValue;        //ġ��Ÿ ���ݷ�
    public int GoldValue;
    public int levelSkillPoint;
    public int currentSceneIdx;
}
[System.Serializable]
public class SkillDataJson
{
    public string DataName;
    public SkillState SkillState;
    public SkillSelecState SkillSelecState;
}

public enum ItemType { Sword, Shield, Hat, Cloth, Pants, Shoes, Neck, Kloak, Ring, Material, Gold, HPPotion, MPPotion, Nothing }
public enum PlayerStat { Level, HP, MP, MaxHP, MaxMP, AttackValue, DefenceValue, FatalProbability, FatalValue, MagciAttackValue }
public enum QuestState { QuestHave, QuestTake, QuestClear, None, QuestNormal }
public enum SkillState { None, Ice, Fire, Electro }
public enum SkillLevelState { None, Lv05, Lv10, Lv20, Lv25, Lv30 }
public enum SkillSelecState { None, Q, E, R, F, C }
[System.Serializable] public class ItemSaveData { public List<ItemDataJson> Items = new List<ItemDataJson>(); }
[System.Serializable] public class QuestSaveData { public List<QuestDataJson> Quests = new List<QuestDataJson>(); }


[System.Serializable]
public class SkillSaveData { public List<SkillDataJson> Skills = new List<SkillDataJson>(); }
