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
    private string itemDataFileName = "ItemData.json"; // 아이템 데이터 JSON 파일 이름
    private string questDataFileName = "QuestSaveData.json";
    private string skillDataFileName = "SkillData.json"; // 스킬 데이터 JSON 파일 이름
    private string playerDataFileName = "PlayerData.json";
    public List<ItemDataJson> Items = new List<ItemDataJson>();
    public List<QuestDataJson> Quests = new List<QuestDataJson>();
    public List<SkillDataJson> Skills = new List<SkillDataJson>();
    public List<RectTransform> uiElements; // 저장할 UI 요소 목록
    public Canvas canvas; // 최상위 Canvas
    // 퀘스트 데이터들
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

            LoadItemData(); // 아이템 데이터 불러오기
            LoadQuest();
            LoadSkillData();
            PlayerDataLoad();
            Debug.Log($"Game data path: {path}");
            Debug.Log($"Item data path: {Path.Combine(Application.persistentDataPath, itemDataFileName)}");
            Debug.Log($"Quest data path: {Path.Combine(Application.persistentDataPath, questDataFileName)}");
            leaderQuestDataJson = FindQuest("기사단장의 명령");
            paladinQuestDataJson = FindQuest("무기착용");
            mariaQuest_01DataJson = FindQuest("기사단원 테스트");
            mariaQuest_02DataJson = FindQuest("마족사냥대로");
            mutantKillerQuest_01DataJson = FindQuest("마족사냥대 테스트");
            mutantKillerQuest_02DataJson = FindQuest("하급 군주 피의 투르악");
            mutantKillerQuest_03DataJson = FindQuest("군단장 파멸의 자르간");
            mutantKillerQuest_04DataJson = FindQuest("군단장 아스모 데우스");
        }
    }
    #region 플레이어 데이터 저장 / 로드

    public void PlayerDataSave(PlayerDataJson data)
    {
        // PlayerDataJson 객체를 JSON 문자열로 변환
        string json = JsonUtility.ToJson(data, true);

        // JSON 문자열을 파일에 저장
        string filePath = Path.Combine(Application.persistentDataPath, playerDataFileName);
        File.WriteAllText(filePath, json);
    }

    // 플레이어 데이터를 로드하는 메서드
    public PlayerDataJson PlayerDataLoad()
    {
        string filePath = Path.Combine(Application.persistentDataPath, playerDataFileName);

        // 파일이 존재하는지 확인
        if (File.Exists(filePath))
        {
            // 파일에서 JSON 문자열을 읽어옴
            string json = File.ReadAllText(filePath);

            // JSON 문자열을 PlayerDataJson 객체로 변환
            playerDataJson = JsonUtility.FromJson<PlayerDataJson>(json);
        }
        else
        {
            // 파일이 존재하지 않으면 새로운 PlayerDataJson 객체를 생성
            playerDataJson = new PlayerDataJson();
        }

        return playerDataJson;
    }

    #endregion
    #region 아이템 저장/로드
    public void SaveItemData(string name, string path, int count, int idx)
    {

        // 새로운 아이템 데이터 생성
        ItemDataJson newItem = new ItemDataJson
        {
            Count = count,
            Name = name,
            Path = path,
            Idx = idx
        };

        // 기존 아이템이 있는지 확인
        ItemDataJson existingItem = itemSaveData.Items.Find(item => item.Name == name);
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
    public ItemDataJson FindItem(string itemName) { var foundItem = Items.Find(item => item.Name == itemName); return foundItem ?? new ItemDataJson(); }
    #endregion
    #region 퀘스트 저장/로드
    public void SaveQuestData(QuestState type, string name_, int questIdx, int killCount = 0, int bossKillCount = 0)
    {
        // 새로운 퀘스트 데이터 생성
        QuestDataJson newQuest = new QuestDataJson
        {
            Name = name_,
            KillCount = killCount,
            bossKillCount = bossKillCount,
            Idx = questIdx,
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
        return foundQuest ?? new QuestDataJson(); // 데이터가 없으면 새로운 QuestDataJson을 반환
    }
    #endregion

    #region 스킬 저장 / 로드
    public void SaveSkillData(string name_, SkillState skillState, SkillSelecState skillSelecState)
    {
        // 기존 스킬이 있는지 확인
        SkillDataJson newSkill = new SkillDataJson
        {
            DataName = name_,
            SkillState = skillState,
            SkillSelecState = skillSelecState
        };
        SkillDataJson existingSkill = skillSaveData.Skills.Find(skill => skill.DataName == name_);

        if (existingSkill != null)
        {
            // 기존 스킬이 있다면 업데이트
            existingSkill.DataName = name_;
            existingSkill.SkillState = skillState;
            existingSkill.SkillSelecState = skillSelecState;
        }
        else
        {
            // 기존 스킬이 없다면 새로운 스킬 추가
            skillSaveData.Skills.Add(newSkill);
        }

        // JSON으로 변환하여 파일에 저장
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
        return foundSkill ?? new SkillDataJson(); // 데이터가 없으면 새로운 SkillDataJson 반환
    }
    #endregion
}
[System.Serializable]
public class GameData
{
    public float[] playerPosition;  //플레이어 위치
    public float[] playerRotation;  //플레이어 Rot
    public int sceneIdx;            //플레이어가 현재 있는 SceneIdx
    public bool IsSave = false;     // 저장을 햿는지 안했는지
}
[System.Serializable]
public class ItemDataJson
{
    public string Name;       // 아이템의 이름
    public int Count;         // 아이템의 개수
    public int Idx;
    public string Path;       // 아이템의 위치 경로
}
[System.Serializable]
public class QuestDataJson
{
    public string Name;     //퀘스트 이름
    public int Idx;
    public int KillCount;   //퀘스트 목표 킬카운트
    public int bossKillCount;
    public QuestState questState;   // 퀘스트 상태
}
[System.Serializable]
public class PlayerDataJson
{
    public float HP;                //체력
    public float MaxHP;             //최대체력
    public float MP;                //마나
    public float MaxMP;             //최대마나
    public float AttackValue;            //데미지
    public float MagicAttackValue;
    public float expValue;        //경험치
    public float maxExpValue;     //최대경험치
    public int Level;             //레벨
    public float DefenceValue;      //방어력
    public float FatalProbability;  //치명타확률
    public float FatalAttackValue;        //치명타 공격력
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
