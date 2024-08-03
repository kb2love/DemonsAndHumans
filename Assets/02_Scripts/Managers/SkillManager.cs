using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;
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
    [SerializeField] Image[] saveImages = new Image[5];
    [SerializeField] Image lastImage;
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
            else if (playerData.Level >= 20 && lv20_01SkillData.SkillState == SkillState.None && lv20_02SkillData.SkillState == SkillState.None)
            {
                LevelUpSelectSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 3);
                LevelUpSelectSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 2);
            }
            else if (playerData.Level >= 20 && lv20_01SkillData.SkillState == SkillState.None) LevelUpSelectSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 3);
            else if (playerData.Level >= 20 && lv20_02SkillData.SkillState == SkillState.None) LevelUpSelectSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 2);
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
        if (lv10SkillData.SkillState != SkillState.None) { SkillCheck(1, lv10SkillData, SkillLevelState.Lv10); }
        if (lv20_01SkillData.SkillState != SkillState.None) { SkillCheck(2, lv20_01SkillData, SkillLevelState.Lv20_01); }
        if (lv20_02SkillData.SkillState != SkillState.None) { SkillCheck(3, lv20_02SkillData, SkillLevelState.Lv20_02); }
        if (lv30SkillData.SkillState != SkillState.None) { SkillCheck(4, lv30SkillData, SkillLevelState.Lv30); }
    }// ������ų�� ������ų���� �˾ƾ��ϰ� ��� �־����� �˾ƾ��� �����ʾҴ��� �־����� �־��ٸ� ��� �־�����
    void SkillCheck(int level, SkillData skillData, SkillLevelState skillLevelState)
    {//������ų�� ������ �˾Ҿ� ���� ��� �־�ߵǴ����� ã�����
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
        {// ������ skillList�� �߰��� ��ų Image
            case SkillState.Ice: skillQuickList[quickIdx].sprite = skillData.IceImage; break;
            case SkillState.Fire: skillQuickList[quickIdx].sprite = skillData.FireImage; break;
            case SkillState.Electro: skillQuickList[quickIdx].sprite = skillData.ElectroImage; break;
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

        lastImage = skillQuickList[quickIdx];

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
    public void IceSpear_05() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 0, SkillState.Ice, ref lv05SkillData.SkillState, SkillLevelState.Lv05, SkillSelec_05, lv05SkillData); }
    public void FireBall_05() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 0, SkillState.Fire, ref lv05SkillData.SkillState, SkillLevelState.Lv05, SkillSelec_05, lv05SkillData); }
    public void ElectroBall_05() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 0, SkillState.Electro, ref lv05SkillData.SkillState, SkillLevelState.Lv05, SkillSelec_05, lv05SkillData); }
    public void IceBall_10() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 1, SkillState.Ice, ref lv10SkillData.SkillState, SkillLevelState.Lv10, SkillSelec_10, lv05SkillData); }
    public void FireBoom_10() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 1, SkillState.Fire, ref lv10SkillData.SkillState, SkillLevelState.Lv10, SkillSelec_10, lv05SkillData); }
    public void ElectroBoom_10() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 1, SkillState.Electro, ref lv10SkillData.SkillState, SkillLevelState.Lv10, SkillSelec_10, lv05SkillData); }
    public void IceAura_20_01() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 2, SkillState.Ice, ref lv20_01SkillData.SkillState, SkillLevelState.Lv20_01, SkillSelec_20_01, lv05SkillData); }
    public void FireWIng_20_01() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 2, SkillState.Fire, ref lv20_01SkillData.SkillState, SkillLevelState.Lv20_01, SkillSelec_20_01, lv05SkillData); }
    public void ElectroAura_20_1() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 2, SkillState.Electro, ref lv20_01SkillData.SkillState, SkillLevelState.Lv20_01, SkillSelec_20_01, lv05SkillData); }
    public void IceKunai_20_02() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 3, SkillState.Ice, ref lv20_02SkillData.SkillState, SkillLevelState.Lv20_02, SkillSelec_20_02, lv05SkillData); }
    public void FireStrike_20_02() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 3, SkillState.Fire, ref lv20_02SkillData.SkillState, SkillLevelState.Lv20_02, SkillSelec_20_02, lv05SkillData); }
    public void ElectroStrike_20_02() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 3, SkillState.Electro, ref lv20_02SkillData.SkillState, SkillLevelState.Lv20_02, SkillSelec_20_02, lv05SkillData); }
    public void IceLaser_30() { LeveUplSkillSelect(fireSkillImageList, electroSkillImageList, iceSkillImageList, 4, SkillState.Ice, ref lv30SkillData.SkillState, SkillLevelState.Lv30, SkillSelec_30, lv05SkillData); }
    public void FireMeteor_30() { LeveUplSkillSelect(iceSkillImageList, electroSkillImageList, fireSkillImageList, 4, SkillState.Fire, ref lv30SkillData.SkillState, SkillLevelState.Lv30, SkillSelec_30, lv05SkillData); }
    public void ElectroArrow_30() { LeveUplSkillSelect(fireSkillImageList, iceSkillImageList, electroSkillImageList, 4, SkillState.Electro, ref lv30SkillData.SkillState,SkillLevelState.Lv30, SkillSelec_30, lv05SkillData); }
    void LeveUplSkillSelect(List<Image> noneSelecList_01, List<Image> noneSelectList_02, List<Image> SelecList, int level, SkillState skillState, ref SkillState state, SkillLevelState skillLevelStatee, UnityAction buttonAction, SkillData skillData)
    {// �������� �ؼ� ��ų�� ����
        noneSelecList_01[level].color = new Color(1, 1, 1, 0.4f);
        noneSelectList_02[level].color = new Color(1, 1, 1, 0.4f);
        noneSelecList_01[level].GetComponent<Button>().enabled = false;
        noneSelectList_02[level].GetComponent<Button>().enabled = false;
        isSelec = false;
        saveImages[level] = SelecList[level];
        SaveSKillData(skillData);
        SelecList[level].GetComponent<Button>().onClick.AddListener(buttonAction);  // �� ��ư�� �߰���
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
        StartCoroutine(SkillSelecButton(SkillSelec_05, 0, lv05SkillData));
        // ��ŸƮ �ڷ�ƾ�� �����ϴµ��� �߰� �ڷ�ƾ�� �߻���Ű�� �ʵ��� ��ư�� �ڷ�ƾ �߰� �����ʸ� �����Ѵ�
        saveImages[0].GetComponent<Button>().onClick.RemoveListener(SkillSelec_05);
    }
    public void SkillSelec_10()
    {
        StartCoroutine(SkillSelecButton(SkillSelec_10, 1, lv10SkillData));
        saveImages[1].GetComponent<Button>().onClick.RemoveListener(SkillSelec_10);
    }
    public void SkillSelec_20_01()
    {
        StartCoroutine(SkillSelecButton(SkillSelec_20_01, 2, lv20_01SkillData));
        saveImages[2].GetComponent<Button>().onClick.RemoveListener(SkillSelec_20_01);
    }
    public void SkillSelec_20_02()
    {
        StartCoroutine(SkillSelecButton(SkillSelec_20_02, 3, lv20_02SkillData));
        saveImages[3].GetComponent<Button>().onClick.RemoveListener(SkillSelec_20_02);
    }
    public void SkillSelec_30()
    {
        StartCoroutine(SkillSelecButton( SkillSelec_30, 4, lv30SkillData));
        saveImages[4].GetComponent<Button>().onClick.RemoveListener(SkillSelec_30);
    }
    #endregion
    IEnumerator SkillSelecButton( UnityAction buttonAction, int level, SkillData skillData)
    {
        while (!isSelec)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Q)) { Selec(0, buttonAction, level,ref skillData, SkillSelecState.Q); break; }
            else if (Input.GetKeyDown(KeyCode.E)) { Selec(1, buttonAction, level,ref skillData, SkillSelecState.E); break; }
            else if (Input.GetKeyDown(KeyCode.R)) { Selec(2, buttonAction, level,ref skillData, SkillSelecState.R); break; }
            else if (Input.GetKeyDown(KeyCode.F)) { Selec(3, buttonAction, level,ref skillData, SkillSelecState.F); break; }
            else if (Input.GetKeyDown(KeyCode.C)) { Selec(4, buttonAction, level,ref skillData, SkillSelecState.C); break; }
        }
    }
    private void Selec(int quickIdx,  UnityAction buttonAction, int levelIdx, ref SkillData skillData, SkillSelecState skillLevelState)
    {
        // ��ų�� �̹� ��ġ�س����� �ִٸ� ������ ��ġ�ߴ����� ��ĭ���� ä���
        switch (skillData.SkillState)
        { // ������ skillList�� �߰��� ��ų Image
            case SkillState.Ice: skillQuickList[quickIdx].sprite = skillData.IceImage; break;
            case SkillState.Fire: skillQuickList[quickIdx].sprite = skillData.FireImage; break;
            case SkillState.Electro: skillQuickList[quickIdx].sprite = skillData.ElectroImage; break;
        }
        skillData.SkillSelecState = skillLevelState;
        lastImage = skillQuickList[quickIdx];
        playerAttack.SkillAdd(quickIdx, levelIdx);
        SaveSKillData(skillData);
        saveImages[levelIdx].GetComponent<Button>().onClick.AddListener(buttonAction);
        isSelec = true;
    }
    private void SaveSKillData(SkillData skillData)
    {
        // ���� ������ ����Ʈ���� ������ �̸��� �����͸� �����ϰ� ���ο� �����͸� �߰��մϴ�.
        skillDataList.RemoveAll(data => data.DataName == skillData.DataName);
        skillDataList.Add(skillData);
        Debug.Log(skillDataList);
        Debug.Log(skillData);
        DataManager.dataInst.SaveSkillData(skillDataList);
    }

}
