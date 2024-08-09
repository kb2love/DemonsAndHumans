
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject shield;
    [SerializeField] Image mpImage;
    NPCPaladinDialouge dialouge;
    Animator animator;
    PlayerController controller;
    PlayerDamage plDamage;
    PlayerAniEvent aniEvent;
    public UnityAction[] skillAction = new UnityAction[5];
    private int attackCount = 0;
    private float comboResetTime = 1.0f; // 연속 공격을 초기화하는 시간
    private float stopAttackTime = 1.25f; // 시간이 지나면 공격카운트를 초기화할 시간
    private float lastAttackTime;       // 현재시간을 저장할 공격한 시간
    private float stopLastAttackTime;   // 현재시간을 저장할 공격을 멈출 시간
    [SerializeField] private float[] levelSkillTime = new float[5]; // 스킬 쿨타임
    [SerializeField] Image[] skillImages;
    private float[] levelLastSkilTime = new float[5];      // 얼음 스킬을 날린 마지막시간을 저장할 시간
    private bool isStop;
    private bool isEquip;
    private bool isInven;
    public bool isSword;
    public bool isShield;
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        aniEvent = GetComponentInChildren<PlayerAniEvent>();
        controller = GetComponent<PlayerController>();
        plDamage = GetComponent<PlayerDamage>();
        if (GameObject.FindWithTag("Paladin") != null)
            dialouge = GameObject.FindWithTag("Paladin").GetComponent<NPCPaladinDialouge>();
        else
            dialouge = null;
    }

    void Update()
    {
        if (!isInven && !isEquip)
        {
            if (isSword)
                SwordAttack();
            if (isShield)
                ShieldBlock();
            if (isSword || isShield)
                Skill();
        }
    }

    private void SwordAttack()
    {
        if (Input.GetMouseButton(0) && Time.time - lastAttackTime > comboResetTime)
        {
            attackCount++;
            controller.IsStop(true);
            animator.SetBool("IsAttack", true);
            TriggerAttackAnimation();
            lastAttackTime = Time.time;
            stopLastAttackTime = Time.time;
        }
        else if (Time.time - stopLastAttackTime > stopAttackTime && isStop)
        {
            attackCount = 0;
            controller.IsStop(false);
            animator.SetBool("IsAttack", false);
            stopLastAttackTime = Time.time;
            isStop = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isStop = true;
        }
    }

    private void ShieldBlock()
    {
        if (Input.GetMouseButton(1))
        {
            if (!plDamage.IsHit)
            {
                animator.SetBool("IsShield", true);
                plDamage.IsShieldOnOff(true);
                controller.IsStop(true);
            }
            else
            {
                animator.SetBool("IsShield", false);
                controller.IsStop(true);
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("IsShield", false);
            plDamage.IsShieldOnOff(false);
            controller.IsStop(false);
        }
    }
    private void Skill()
    {
        if (Input.GetKeyDown(KeyCode.Q) && skillAction[0] != null) { skillAction[0](); }
        else if (Input.GetKeyDown(KeyCode.E) && skillAction[1] != null) { skillAction[1](); }
        else if (Input.GetKeyDown(KeyCode.R) && skillAction[2] != null) { skillAction[2](); }
        else if (Input.GetKeyDown(KeyCode.F) && skillAction[3] != null) { skillAction[3](); }
        else if (Input.GetKeyDown(KeyCode.C) && skillAction[4] != null) { skillAction[4](); }
    }
    public void SkillAdd(int qucikIdx, int levelIdx)
    {
        switch (levelIdx)
        {
            case 0: skillAction[qucikIdx] = Level05SkillCasting; break;
            case 1: skillAction[qucikIdx] = Level10SkillCasting; break;
            case 2: skillAction[qucikIdx] = Level20SkillCasting; break;
            case 3: skillAction[qucikIdx] = Level25SkillCasting; break;
            case 4: skillAction[qucikIdx] = Level30SkillCasting; break;
        }
    }

    private void Level05SkillCasting() { if (Time.time - levelLastSkilTime[0] > levelSkillTime[0] && GameManager.GM.playerDataJson.MP >= 20) SkillCasting("Lv05SkillTrigger", 0, 20, 1, 0); }
    private void Level10SkillCasting() { if (Time.time - levelLastSkilTime[1] > levelSkillTime[1] && GameManager.GM.playerDataJson.MP >= 50) SkillCasting("Lv10SkillTrigger", 1, 50, 1.5f, 1); }
    private void Level20SkillCasting() { if (Time.time - levelLastSkilTime[2] > levelSkillTime[2] && GameManager.GM.playerDataJson.MP >= 100) SkillCasting("Lv20SkillTrigger", 2, 100, 1, 2); }
    private void Level25SkillCasting() { if (Time.time - levelLastSkilTime[3] > levelSkillTime[3] && GameManager.GM.playerDataJson.MP >= 100) SkillCasting("Lv25SkillTrigger", 3, 100, 2, 3); }
    private void Level30SkillCasting() { if (Time.time - levelLastSkilTime[4] > levelSkillTime[4] && GameManager.GM.playerDataJson.MP >= 200) SkillCasting("Lv30SkillTrigger", 4, 200, 2, 4); }
    private void SkillCasting(string aniTriggerName, int imageIdx, int mp, float stopeTime, int lastSkillTimeIdx)
    {//플레이어가 찍은 레벨스킬로 스킬을 코루틴을 정하고 애니메이션을 실행시킨다
        controller.IsStop(true);
        animator.SetTrigger(aniTriggerName);
        skillImages[imageIdx].fillAmount = 0;
        DOTween.To(() => skillImages[imageIdx].fillAmount, x => skillImages[imageIdx].fillAmount = x, 1f, levelSkillTime[imageIdx])
       .SetEase(Ease.Linear); // 선형 보간을 사용하여 일정한 속도로 변화
        GameManager.GM.playerDataJson.MP -= mp;
        mpImage.fillAmount = GameManager.GM.playerDataJson.MP / GameManager.GM.playerDataJson.MaxMP;
        DataManager.dataInst.PlayerDataSave(GameManager.GM.playerDataJson);
        Invoke("IsMove", stopeTime);
        levelLastSkilTime[lastSkillTimeIdx] = Time.time;
    }
    public void SkillImageChange(int level, Sprite skillImage) { skillImages[level].sprite = skillImage; }
    void TriggerAttackAnimation()
    {
        switch (attackCount)
        {
            case 1:
                animator.SetTrigger("AttackDown");
                break;
            case 2:
                animator.SetTrigger("AttackUp");
                break;
            case 3:
                animator.SetTrigger("AttackRotate");
                attackCount = 0;
                break;
        }
    }
    public void IsInventory(bool _isInven)
    {
        isInven = _isInven;
    }
    public void IsEquipment(bool _isEquip)
    {
        isEquip = _isEquip;
    }
    void IsMove()
    {
        controller.IsStop(false);
    }
    public void IsSword(bool _IsSword)
    {
        isSword = _IsSword;
        animator.SetBool("GetSword", isSword);
        sword.SetActive(isSword);
        if (isSword)
        {
            if (dialouge != null)
                dialouge.WeaponWear();
        }
    }
    public void IsShield(bool _IsShield)
    {
        isShield = _IsShield;
        animator.SetBool("GetShield", isShield);
        shield.SetActive(isShield);
    }
}
