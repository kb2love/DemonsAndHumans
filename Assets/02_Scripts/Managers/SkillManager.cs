using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
// ���� �Ѿ�ų� ������ �����Ҷ� ��ų�� �ʱ�ȭ�ϴ� �� ������
public class SkillManager : MonoBehaviour
{
    public static SkillManager skillInst;
    SkillState skillState;
    [SerializeField] PlayerData playerData;
    [SerializeField] SkillData lv05SkillData;
    [SerializeField] SkillData lv10SkillData;
    [SerializeField] SkillData lv20_01SkillData;
    [SerializeField] SkillData lv20_02SkillData;
    [SerializeField] SkillData lv30SkillData;
    [SerializeField] Transform iceSkillImage;
    [SerializeField] Transform fireSkillImage;
    [SerializeField] Transform electroSkillImage;
    [SerializeField] Sprite skillWindow;
    List<Image> iceSkillImageList = new List<Image>();
    List<Image> fireSkillImageList = new List<Image>();
    List<Image> electroSkillImageList = new List<Image>();
    [SerializeField] List<Image> skillQuickList = new List<Image>();
    [SerializeField] List<Image> skillImageSaveList = new List<Image>();  // ��ų�� ���� �� List�� ������ List ������� iceSkill�� �����ϸ� iceSkill�� �����Ѵ�
    [SerializeField] Image lastImage;
    int[] skillSelecIdx = new int[5];  // ��ų ���� �� int��
    // ��Ʈ�� �������� State�� ����
    PlayerAniEvent aniEvent;
    PlayerAttack playerAttack;
    bool isSelec = false;
    private List<SkillData> skillDataList = new List<SkillData>();
    private void Awake()
    {
        skillInst = this;
    }
    public void Initialize()
    {
        for (int i = 1; i < iceSkillImage.childCount; i++) { iceSkillImageList.Add(iceSkillImage.GetChild(i).GetComponent<Image>()); }
        for (int i = 1; i < fireSkillImage.childCount; i++) { fireSkillImageList.Add(fireSkillImage.GetChild(i).GetComponent<Image>()); }
        for (int i = 1; i < electroSkillImage.childCount; i++) { electroSkillImageList.Add(electroSkillImage.GetChild(i).GetComponent<Image>()); }
        aniEvent = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerAniEvent>();
        playerAttack = aniEvent.GetComponentInParent<PlayerAttack>();
       // ��ų ����Ʈ �� ������ ���� ��ų ����
    if (playerData.levelSkillPoint > 0)
    {
        if (playerData.Level >= 30 && lv05SkillData.SkillState == SkillState.None) LevelUpSelectSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 4);
        else if (playerData.Level >= 20 && lv10SkillData.SkillState == SkillState.None) LevelUpSelectSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 3);
        else if (playerData.Level >= 20 && lv20_01SkillData.SkillState == SkillState.None) LevelUpSelectSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 2);
        else if (playerData.Level >= 10 && lv20_02SkillData.SkillState == SkillState.None) LevelUpSelectSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 1);
        else if (playerData.Level >= 5 && lv30SkillData.SkillState == SkillState.None) LevelUpSelectSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 0);
    }
        DataManager.dataInst.LoadSkillData();
        lv05SkillData = DataManager.dataInst.GetSkillDataByName("Lv05SkillData", lv05SkillData);
        lv10SkillData = DataManager.dataInst.GetSkillDataByName("Lv10SkillData", lv10SkillData);
        lv20_01SkillData = DataManager.dataInst.GetSkillDataByName("Lv20_01SkillData", lv20_01SkillData);
        lv20_02SkillData = DataManager.dataInst.GetSkillDataByName("Lv20_02SkillData", lv20_02SkillData);
        lv30SkillData = DataManager.dataInst.GetSkillDataByName("Lv30SkillData", lv30SkillData);
        if (lv05SkillData.SkillState != SkillState.None) { SkillCheck(0, lv05SkillData, SkillLevelState.Lv05); }
        if (lv10SkillData.SkillState != SkillState.None) { Debug.Log(1); SkillCheck(1, lv10SkillData, SkillLevelState.Lv10); }
        if (lv20_01SkillData.SkillState != SkillState.None) { SkillCheck(2, lv20_01SkillData, SkillLevelState.Lv20_01); }
        if (lv20_02SkillData.SkillState != SkillState.None) { SkillCheck(3, lv20_02SkillData, SkillLevelState.Lv20_02); }
        if (lv30SkillData.SkillState != SkillState.None) { SkillCheck(4, lv30SkillData, SkillLevelState.Lv30); }
    }// ������ų�� ������ų���� �˾ƾ��ϰ� ��� �־����� �˾ƾ��� �����ʾҴ��� �־����� �־��ٸ� ��� �־�����
    void SkillCheck(int level, SkillData skillData, SkillLevelState skillLevelState)
    {//������ų�� ������ �˾Ҿ� ���� ��� �־�ߵǴ����� ã�����
        Debug.Log(skillData.SkillState.ToString());
        Debug.Log(skillData.SkillSelecState.ToString());
        switch (skillData.SkillState)
        {// ������ skillList�� �߰��� ��ų Image
            case SkillState.Ice: skillQuickList[skillSelecIdx[level]].sprite = skillData.IceImage; break;
            case SkillState.Fire: skillQuickList[skillSelecIdx[level]].sprite = skillData.FireImage; break;
            case SkillState.Electro: skillQuickList[skillSelecIdx[level]].sprite = skillData.ElectroImage; break;
        }
        switch (skillData.SkillSelecState)
        {
            case SkillSelecState.Q: playerAttack.SkillAdd(0, level); break;
            case SkillSelecState.E: playerAttack.SkillAdd(1, level); break;
            case SkillSelecState.R: playerAttack.SkillAdd(2, level); break;
            case SkillSelecState.F: playerAttack.SkillAdd(3, level); break;
            case SkillSelecState.C: playerAttack.SkillAdd(4, level); break;
        }
        aniEvent.LevelSkillChange(skillData.SkillState, skillLevelState);

        lastImage = skillQuickList[skillSelecIdx[level]];

    }
    public void Level5()
    {
        playerData.levelSkillPoint++;
        LevelUpSelectSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 0);
    }

    public void Level10()
    {
        playerData.levelSkillPoint++;
        LevelUpSelectSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 1);
    }
    public void Level20()
    {
        playerData.levelSkillPoint++;
        if (lv20_01SkillData.SkillState == SkillState.None) LevelUpSelectSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 2);
        if (lv20_02SkillData.SkillState == SkillState.None) LevelUpSelectSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 3);
    }
    public void Level30()
    {
        playerData.levelSkillPoint++;
        LevelUpSelectSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 4);
    }
    public void IceSpear_05() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 0, SkillState.Ice, ref lv05SkillData.SkillState, 0, SkillLevelState.Lv05, SkillSelec_05, lv05SkillData); }
    public void FireBall_05() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 0, SkillState.Fire, ref lv05SkillData.SkillState, 1, SkillLevelState.Lv05, SkillSelec_05, lv05SkillData); }
    public void ElectroBall_05() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 0, SkillState.Electro, ref lv05SkillData.SkillState, 2, SkillLevelState.Lv05, SkillSelec_05, lv05SkillData); }
    public void IceBall_10() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 1, SkillState.Ice, ref lv10SkillData.SkillState, 3, SkillLevelState.Lv10, SkillSelec_10, lv05SkillData); }
    public void FireBoom_10() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 1, SkillState.Fire, ref lv10SkillData.SkillState, 4, SkillLevelState.Lv10, SkillSelec_10, lv05SkillData); }
    public void ElectroBoom_10() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 1, SkillState.Electro, ref lv10SkillData.SkillState, 5, SkillLevelState.Lv10, SkillSelec_10, lv05SkillData); }
    public void IceAura_20_01() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 2, SkillState.Ice, ref lv20_01SkillData.SkillState, 6, SkillLevelState.Lv20_01, SkillSelec_20_01, lv05SkillData); }
    public void FireWIng_20_01() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 2, SkillState.Fire, ref lv20_01SkillData.SkillState, 7, SkillLevelState.Lv20_01, SkillSelec_20_01, lv05SkillData); }
    public void ElectroAura_20_1() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 2, SkillState.Electro, ref lv20_01SkillData.SkillState, 8, SkillLevelState.Lv20_01, SkillSelec_20_01, lv05SkillData); }
    public void IceKunai_20_02() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 3, SkillState.Ice, ref lv20_02SkillData.SkillState, 9, SkillLevelState.Lv20_02, SkillSelec_20_02, lv05SkillData); }
    public void FireStrike_20_02() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 3, SkillState.Fire, ref lv20_02SkillData.SkillState, 10, SkillLevelState.Lv20_02, SkillSelec_20_02, lv05SkillData); }
    public void ElectroStrike_20_02() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 3, SkillState.Electro, ref lv20_02SkillData.SkillState, 11, SkillLevelState.Lv20_02, SkillSelec_20_02, lv05SkillData); }
    public void IceLaser_30() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 4, SkillState.Ice, ref lv30SkillData.SkillState, 12, SkillLevelState.Lv30, SkillSelec_30, lv05SkillData); }
    public void FireMeteor_30() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 4, SkillState.Fire, ref lv30SkillData.SkillState, 13, SkillLevelState.Lv30, SkillSelec_30, lv05SkillData); }
    public void ElectroArrow_30() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 4, SkillState.Electro, ref lv30SkillData.SkillState, 14, SkillLevelState.Lv30, SkillSelec_30, lv05SkillData); }
    void LeveUplSkillSelect(List<Image> noneSelecList_01, List<Image> noneSelectList_02, List<Image> SelecList, int idx, SkillState skillState, ref SkillState state, int _skillSelecIdx
        , SkillLevelState skillLevelStatee, UnityAction buttonAction, SkillData skillData)
    {// �������� �ؼ� ��ų�� ����
        noneSelecList_01[idx].color = new Color(1, 1, 1, 0.4f);
        noneSelectList_02[idx].color = new Color(1, 1, 1, 0.4f);
        noneSelecList_01[idx].GetComponent<Button>().enabled = false;
        noneSelectList_02[idx].GetComponent<Button>().enabled = false;
        isSelec = false;
        skillImageSaveList.Add(SelecList[idx]);
        switch (skillLevelStatee)
        {// ��ų �����Կ� �� ��ų�� Image�� Idx�� 
            case SkillLevelState.Lv05: skillSelecIdx[0] = _skillSelecIdx; break;
            case SkillLevelState.Lv10: skillSelecIdx[1] = _skillSelecIdx; break;
            case SkillLevelState.Lv20_01: skillSelecIdx[2] = _skillSelecIdx; break;
            case SkillLevelState.Lv20_02: skillSelecIdx[3] = _skillSelecIdx; break;
            case SkillLevelState.Lv30: skillSelecIdx[4] = _skillSelecIdx; break;
        }
        SaveSKillData(skillData);
        SelecList[idx].GetComponent<Button>().onClick.AddListener(buttonAction);  // �� ��ư�� �߰���
        state = skillState;
        aniEvent.LevelSkillChange(state, skillLevelStatee);
        playerData.levelSkillPoint--;
    }

    void LevelUpSelectSkill(List<Image> iceList, List<Image> fireList, List<Image> electroList, int idx)
    {
        iceList[idx].color = new Color(1, 1, 1, 1);
        fireList[idx].color = new Color(1, 1, 1, 1);
        electroList[idx].color = new Color(1, 1, 1, 1);
        iceList[idx].GetComponent<Button>().enabled = true;
        fireList[idx].GetComponent<Button>().enabled = true;
        electroList[idx].GetComponent<Button>().enabled = true;
    }
    #region ��ų�� ���� ���� �� ��ų�� ��ư�� �� UnityAction��
    public void SkillSelec_05()
    {// ��ųƮ���� �ִ� ��ų�� Ŭ���Ͽ� ���â���� �ִ� �޼���
        StartCoroutine(SkillSelecButton(skillSelecIdx[0], SkillSelec_05, 0, lv05SkillData));
        // ��ŸƮ �ڷ�ƾ�� �����ϴµ��� �߰� �ڷ�ƾ�� �߻���Ű�� �ʵ��� ��ư�� �ڷ�ƾ �߰� �����ʸ� �����Ѵ�
        skillImageSaveList[0].GetComponent<Button>().onClick.RemoveListener(SkillSelec_05);
    }
    public void SkillSelec_10()
    {
        StartCoroutine(SkillSelecButton(skillSelecIdx[1], SkillSelec_10, 1, lv10SkillData));
        skillImageSaveList[1].GetComponent<Button>().onClick.RemoveListener(SkillSelec_10);
    }
    public void SkillSelec_20_01()
    {
        StartCoroutine(SkillSelecButton(skillSelecIdx[2], SkillSelec_20_01, 2, lv20_01SkillData));
        skillImageSaveList[2].GetComponent<Button>().onClick.RemoveListener(SkillSelec_20_01);
    }
    public void SkillSelec_20_02()
    {
        StartCoroutine(SkillSelecButton(skillSelecIdx[3], SkillSelec_20_02, 3, lv20_02SkillData));
        skillImageSaveList[3].GetComponent<Button>().onClick.RemoveListener(SkillSelec_20_02);
    }
    public void SkillSelec_30()
    {
        StartCoroutine(SkillSelecButton(skillSelecIdx[4], SkillSelec_30, 4, lv30SkillData));
        skillImageSaveList[4].GetComponent<Button>().onClick.RemoveListener(SkillSelec_30);
    }
    #endregion
    IEnumerator SkillSelecButton(int skillSelecIdx, UnityAction buttonAction, int level, SkillData skillData)
    {
        while (!isSelec)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Q)) { Selec(0, skillSelecIdx, buttonAction, level, skillData); break; }
            else if (Input.GetKeyDown(KeyCode.E)) { Selec(1, skillSelecIdx, buttonAction, level, skillData); break; }
            else if (Input.GetKeyDown(KeyCode.R)) { Selec(2, skillSelecIdx, buttonAction, level, skillData); break; }
            else if (Input.GetKeyDown(KeyCode.F)) { Selec(3, skillSelecIdx, buttonAction, level, skillData); break; }
            else if (Input.GetKeyDown(KeyCode.C)) { Selec(4, skillSelecIdx, buttonAction, level, skillData); break; }
        }
    }
    private void Selec(int quickIdx, int skillSelecIdx, UnityAction buttonAction, int levelIdx, SkillData skillData)
    {
        // ��ų�� �̹� ��ġ�س����� �ִٸ� ������ ��ġ�ߴ����� ��ĭ���� ä���
        if (lastImage != null) { lastImage.sprite = skillWindow; }
        switch (skillData.SkillState)
        { // ������ skillList�� �߰��� ��ų Image
            case SkillState.Ice: skillQuickList[quickIdx].sprite = skillData.IceImage; break;
            case SkillState.Fire: skillQuickList[quickIdx].sprite = skillData.FireImage; break;
            case SkillState.Electro: skillQuickList[quickIdx].sprite = skillData.ElectroImage; break;
        }

        lastImage = skillQuickList[quickIdx];
        playerAttack.SkillAdd(quickIdx, levelIdx);
        SaveSKillData(skillData);
        skillImageSaveList[levelIdx].GetComponent<Button>().onClick.AddListener(buttonAction);
        isSelec = true;
    }
    private void SaveSKillData(SkillData skillData)
    {
        // ���� ������ ����Ʈ���� ������ �̸��� �����͸� �����ϰ� ���ο� �����͸� �߰��մϴ�.
        skillDataList.RemoveAll(data => data.DataName == skillData.DataName);
        skillDataList.Add(skillData);

        DataManager.dataInst.SaveSkillData(skillDataList);
    }

}
