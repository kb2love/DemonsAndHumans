using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
// 신이 넘어가거나 게임을 시작할때 스킬을 초기화하는 걸 만들자
public class SkillManager : MonoBehaviour
{
    public static SkillManager skillInst;
    SkillState skillState;
    [SerializeField] PlayerData playerData;
    [SerializeField] SkillData lv05SkillData;
    [SerializeField] SkillData lv10SkillData;
    [SerializeField] SkillData lv20SkillData;
    [SerializeField] SkillData lv25SkillData;
    [SerializeField] SkillData lv30SkillData;
    [SerializeField] Transform iceSkillImage;
    [SerializeField] Transform fireSkillImage;
    [SerializeField] Transform electroSkillImage;
    [SerializeField] Sprite skillWindow;
    List<Image> iceSkillImageList = new List<Image>();
    List<Image> fireSkillImageList = new List<Image>();
    List<Image> electroSkillImageList = new List<Image>();
    [SerializeField] List<Image> skillQuickList = new List<Image>();
    [SerializeField] Image[] saveImages = new Image[5];
    [SerializeField] Text skillPointText;
    // 인트로 하지말고 State로 하자
    PlayerAniEvent aniEvent;
    PlayerAttack playerAttack;
    bool isSelec = false;
    SkillDataJson[] skillDataJson = new SkillDataJson[5];
    GameManager GM;
    private void Awake()
    {
        skillInst = this;
    }
    public void Initialize()
    {
        GM = GameManager.GM;
        for (int i = 1; i < iceSkillImage.childCount; i++) { iceSkillImageList.Add(iceSkillImage.GetChild(i).GetComponent<Image>()); }
        for (int i = 1; i < fireSkillImage.childCount; i++) { fireSkillImageList.Add(fireSkillImage.GetChild(i).GetComponent<Image>()); }
        for (int i = 1; i < electroSkillImage.childCount; i++) { electroSkillImageList.Add(electroSkillImage.GetChild(i).GetComponent<Image>()); }
        aniEvent = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerAniEvent>();
        playerAttack = aniEvent.GetComponentInParent<PlayerAttack>();
        skillDataJson[0] = DataManager.dataInst.FindSkill(lv05SkillData.DataName);
        skillDataJson[1] = DataManager.dataInst.FindSkill(lv10SkillData.DataName);
        skillDataJson[2] = DataManager.dataInst.FindSkill(lv20SkillData.DataName);
        skillDataJson[3] = DataManager.dataInst.FindSkill(lv25SkillData.DataName);
        skillDataJson[4] = DataManager.dataInst.FindSkill(lv30SkillData.DataName);
        // 스킬 포인트 및 레벨에 따라 스킬 선택
        if (GM.playerDataJson.levelSkillPoint > 0)
        {
            if (GM.playerDataJson.Level >= 30 && skillDataJson[4].SkillState == SkillState.None) LevelUpSelectSkill(4);
            else if (GM.playerDataJson.Level >= 25 && skillDataJson[3].SkillState == SkillState.None) LevelUpSelectSkill(3);
            else if (GM.playerDataJson.Level >= 20 && skillDataJson[2].SkillState == SkillState.None) LevelUpSelectSkill(2);
            else if (GM.playerDataJson.Level >= 10 && skillDataJson[1].SkillState == SkillState.None) LevelUpSelectSkill(1);
            else if (GM.playerDataJson.Level >= 5 && skillDataJson[0].SkillState == SkillState.None) LevelUpSelectSkill(0);
        }
        DataManager.dataInst.LoadSkillData();
        if (skillDataJson[0].SkillState != SkillState.None) { SkillCheck(0, skillDataJson[0], lv05SkillData, SkillLevelState.Lv05); }
        if (skillDataJson[1].SkillState != SkillState.None) { SkillCheck(1, skillDataJson[1], lv10SkillData, SkillLevelState.Lv10); }
        if (skillDataJson[2].SkillState != SkillState.None) { SkillCheck(2, skillDataJson[2], lv20SkillData, SkillLevelState.Lv20); }
        if (skillDataJson[3].SkillState != SkillState.None) { SkillCheck(3, skillDataJson[3], lv25SkillData, SkillLevelState.Lv25); }
        if (skillDataJson[4].SkillState != SkillState.None) { SkillCheck(4, skillDataJson[4], lv30SkillData, SkillLevelState.Lv30); }
    }
    void SkillCheck(int level, SkillDataJson skillData, SkillData skillData02, SkillLevelState skillLevelState)
    {// 스킬을 이미찍었다면 확인한다면
        int quickIdx = 0;
        switch (skillData.SkillSelecState)
        {
            case SkillSelecState.Q: quickIdx = 0; break;
            case SkillSelecState.E: quickIdx = 1; break;
            case SkillSelecState.R: quickIdx = 2; break;
            case SkillSelecState.F: quickIdx = 3; break;
            case SkillSelecState.C: quickIdx = 4; break;
        }
        switch (skillData.SkillState)
        {// 퀵슬롯 skillList에 추가할 스킬 Image
            case SkillState.Ice:
                skillQuickList[quickIdx].sprite = skillData02.IceImage;
                playerAttack.SkillImageChange(level, skillData02.IceImage);
                skillQuickList[quickIdx].transform.GetChild(0).GetComponent<Image>().sprite = skillData02.IceImage; break;
            case SkillState.Fire:
                skillQuickList[quickIdx].sprite = skillData02.FireImage;
                playerAttack.SkillImageChange(level, skillData02.FireImage);
                skillQuickList[quickIdx].transform.GetChild(0).GetComponent<Image>().sprite = skillData02.FireImage; break;
            case SkillState.Electro:
                skillQuickList[quickIdx].sprite = skillData02.ElectroImage;
                playerAttack.SkillImageChange(level, skillData02.ElectroImage);
                skillQuickList[quickIdx].transform.GetChild(0).GetComponent<Image>().sprite = skillData02.ElectroImage; break;
        }
        switch (skillData.SkillSelecState)
        {
            case SkillSelecState.Q: playerAttack.SkillAdd(0, level); break;
            case SkillSelecState.E: playerAttack.SkillAdd(1, level); break;
            case SkillSelecState.R: playerAttack.SkillAdd(2, level); break;
            case SkillSelecState.F: playerAttack.SkillAdd(3, level); break;
            case SkillSelecState.C: playerAttack.SkillAdd(4, level); break;
        }
        AlreadySkillSelec(level, skillData.SkillState);
        aniEvent.LevelSkillChange(skillData.SkillState, skillLevelState);
    }
    public void Level5()
    {
        GM.playerDataJson.levelSkillPoint++;
        LevelUpSelectSkill(0);
    }

    public void Level10()
    {
        GM.playerDataJson.levelSkillPoint++;
        LevelUpSelectSkill(1);
    }
    public void Level20()
    {
        GM.playerDataJson.levelSkillPoint++;
        LevelUpSelectSkill(2);

    }
    public void Level25()
    {
        GM.playerDataJson.levelSkillPoint++;
        LevelUpSelectSkill(3);
    }
    public void Level30()
    {
        GM.playerDataJson.levelSkillPoint++;
        LevelUpSelectSkill(4);
    }

    public void IceSpear_05() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 0, SkillState.Ice, ref skillDataJson[0], SkillLevelState.Lv05, SkillSelec_05, lv05SkillData); }
    public void FireBall_05() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 0, SkillState.Fire, ref skillDataJson[0], SkillLevelState.Lv05, SkillSelec_05, lv05SkillData); }
    public void ElectroBall_05() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 0, SkillState.Electro, ref skillDataJson[0], SkillLevelState.Lv05, SkillSelec_05, lv05SkillData); }
    public void IceBall_10() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 1, SkillState.Ice, ref skillDataJson[1], SkillLevelState.Lv10, SkillSelec_10, lv10SkillData); }
    public void FireBoom_10() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 1, SkillState.Fire, ref skillDataJson[1], SkillLevelState.Lv10, SkillSelec_10, lv10SkillData); }
    public void ElectroBoom_10() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 1, SkillState.Electro, ref skillDataJson[1], SkillLevelState.Lv10, SkillSelec_10, lv10SkillData); }
    public void IceAura_20() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 2, SkillState.Ice, ref skillDataJson[2], SkillLevelState.Lv20, SkillSelec_20, lv20SkillData); }
    public void FireWIng_20() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 2, SkillState.Fire, ref skillDataJson[2], SkillLevelState.Lv20, SkillSelec_20, lv20SkillData); }
    public void ElectroAura_20() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 2, SkillState.Electro, ref skillDataJson[2], SkillLevelState.Lv20, SkillSelec_20, lv20SkillData); }
    public void IceKunai_25() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 3, SkillState.Ice, ref skillDataJson[3], SkillLevelState.Lv25, SkillSelec_25, lv25SkillData); }
    public void FireStrike_25() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 3, SkillState.Fire, ref skillDataJson[3], SkillLevelState.Lv25, SkillSelec_25, lv25SkillData); }
    public void ElectroStrike_25() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 3, SkillState.Electro, ref skillDataJson[3], SkillLevelState.Lv25, SkillSelec_25, lv25SkillData); }
    public void IceLaser_30() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 4, SkillState.Ice, ref skillDataJson[4], SkillLevelState.Lv30, SkillSelec_30, lv30SkillData); }
    public void FireMeteor_30() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 4, SkillState.Fire, ref skillDataJson[4], SkillLevelState.Lv30, SkillSelec_30, lv30SkillData); }
    public void ElectroArrow_30() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 4, SkillState.Electro, ref skillDataJson[4], SkillLevelState.Lv30, SkillSelec_30, lv30SkillData); }
    void LeveUplSkillSelect(List<Image> noneSelecList_01, List<Image> noneSelectList_02, List<Image> SelecList, int level, SkillState skillState, ref SkillDataJson skillDataJson, SkillLevelState skillLevelStatee, UnityAction buttonAction, SkillData skillData)
    {// 레벨업을 해서 스킬을 고르면
        noneSelecList_01[level].color = new Color(1, 1, 1, 0.4f);
        noneSelectList_02[level].color = new Color(1, 1, 1, 0.4f);
        noneSelecList_01[level].GetComponent<Button>().enabled = false;
        noneSelectList_02[level].GetComponent<Button>().enabled = false;
        isSelec = false;
        saveImages[level] = SelecList[level];
        SelecList[level].GetComponent<Button>().onClick.AddListener(buttonAction);  // 이 버튼을 추가함
        skillDataJson.SkillState = skillState;
        Debug.Log(skillDataJson);
        Debug.Log(skillData.DataName);
        DataManager.dataInst.SaveSkillData(skillData.DataName, skillDataJson.SkillState, skillDataJson.SkillSelecState);
        aniEvent.LevelSkillChange(skillDataJson.SkillState, skillLevelStatee);
        if (GM.playerDataJson.levelSkillPoint > 0)
        {
            GM.playerDataJson.levelSkillPoint--;
            skillPointText.text = "스킬포인트 : " + GM.playerDataJson.levelSkillPoint;
        }
    }
    void AlreadySkillSelec(int level, SkillState skillState)
    {
        switch (skillState)
        {
            case SkillState.Ice: AlreadySkillSelecMethod(level, 1, 0.4f, 0.4f); break;
            case SkillState.Fire: AlreadySkillSelecMethod(level, 0.4f, 1, 0.4f); break;
            case SkillState.Electro: AlreadySkillSelecMethod(level, 0.4f, 0.4f, 1); break;
        }
    }

    private void AlreadySkillSelecMethod(int level, float ice, float fire, float electro)
    {
        iceSkillImageList[level].color = new Color(1, 1, 1, ice);
        fireSkillImageList[level].color = new Color(1, 1, 1, fire);
        electroSkillImageList[level].color = new Color(1, 1, 1, electro);
    }

    void LevelUpSelectSkill(int idx)
    {
        iceSkillImageList[idx].color = new Color(1, 1, 1, 1);
        fireSkillImageList[idx].color = new Color(1, 1, 1, 1);
        electroSkillImageList[idx].color = new Color(1, 1, 1, 1);
        iceSkillImageList[idx].GetComponent<Button>().enabled = true;
        fireSkillImageList[idx].GetComponent<Button>().enabled = true;
        electroSkillImageList[idx].GetComponent<Button>().enabled = true;
    }
    #region 스킬을 배우고 나서 그 스킬의 버튼에 들어갈 UnityAction들
    public void SkillSelec_05()
    {// 스킬트리에 있는 스킬을 클릭하여 사용창으로 넣는 메서드
        StartCoroutine(SkillSelecButton(SkillSelec_05, 0, lv05SkillData, skillDataJson[0]));
        // 스타트 코루틴을 시작하는동안 추가 코루틴을 발생시키지 않도록 버튼에 코루틴 추가 리스너를 삭제한다
        saveImages[0].GetComponent<Button>().onClick.RemoveListener(SkillSelec_05);
    }
    public void SkillSelec_10()
    {
        StartCoroutine(SkillSelecButton(SkillSelec_10, 1, lv10SkillData, skillDataJson[1]));
        saveImages[1].GetComponent<Button>().onClick.RemoveListener(SkillSelec_10);
    }
    public void SkillSelec_20()
    {
        StartCoroutine(SkillSelecButton(SkillSelec_20, 2, lv20SkillData, skillDataJson[2]));
        saveImages[2].GetComponent<Button>().onClick.RemoveListener(SkillSelec_20);
    }
    public void SkillSelec_25()
    {
        StartCoroutine(SkillSelecButton(SkillSelec_25, 3, lv25SkillData, skillDataJson[3]));
        saveImages[3].GetComponent<Button>().onClick.RemoveListener(SkillSelec_25);
    }
    public void SkillSelec_30()
    {
        StartCoroutine(SkillSelecButton(SkillSelec_30, 4, lv30SkillData, skillDataJson[4]));
        saveImages[4].GetComponent<Button>().onClick.RemoveListener(SkillSelec_30);
    }
    #endregion
    IEnumerator SkillSelecButton(UnityAction buttonAction, int level, SkillData skillData, SkillDataJson skillDataJson)
    {
        while (!isSelec)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Q)) { Selec(0, buttonAction, level, ref skillData,ref skillDataJson, SkillSelecState.Q); break; }
            else if (Input.GetKeyDown(KeyCode.E)) { Selec(1, buttonAction, level, ref skillData, ref skillDataJson, SkillSelecState.E); break; }
            else if (Input.GetKeyDown(KeyCode.R)) { Selec(2, buttonAction, level, ref skillData, ref skillDataJson, SkillSelecState.R); break; }
            else if (Input.GetKeyDown(KeyCode.F)) { Selec(3, buttonAction, level, ref skillData, ref skillDataJson, SkillSelecState.F); break; }
            else if (Input.GetKeyDown(KeyCode.C)) { Selec(4, buttonAction, level, ref skillData, ref skillDataJson, SkillSelecState.C); break; }
        }
    }
    private void Selec(int quickIdx, UnityAction buttonAction, int levelIdx, ref SkillData skillData,ref SkillDataJson skillDataJson, SkillSelecState skillLevelState)
    {
        // 스킬을 이미 배치해논적이 있다면 그전에 배치했던곳은 빈칸으로 채운다

        switch (skillDataJson.SkillState)
        {// 퀵슬롯 skillList에 추가할 스킬 Image
            case SkillState.Ice:
                skillQuickList[quickIdx].sprite = skillData.IceImage;
                playerAttack.SkillImageChange(quickIdx, skillData.IceImage);
                skillQuickList[quickIdx].transform.GetChild(0).GetComponent<Image>().sprite = skillData.IceImage; break;
            case SkillState.Fire:
                skillQuickList[quickIdx].sprite = skillData.FireImage;
                playerAttack.SkillImageChange(quickIdx, skillData.FireImage);
                skillQuickList[quickIdx].transform.GetChild(0).GetComponent<Image>().sprite = skillData.FireImage; break;
            case SkillState.Electro:
                skillQuickList[quickIdx].sprite = skillData.ElectroImage;
                playerAttack.SkillImageChange(quickIdx, skillData.ElectroImage);
                skillQuickList[quickIdx].transform.GetChild(0).GetComponent<Image>().sprite = skillData.ElectroImage; break;
        }
        skillDataJson.SkillSelecState = skillLevelState;
        playerAttack.SkillAdd(quickIdx, levelIdx);
        DataManager.dataInst.SaveSkillData( skillData.DataName, skillDataJson.SkillState, skillDataJson.SkillSelecState);
        saveImages[levelIdx].GetComponent<Button>().onClick.AddListener(buttonAction);
        isSelec = true;
    }
}
