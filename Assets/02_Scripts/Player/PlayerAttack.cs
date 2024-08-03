
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
    private float levelLastSkilTime;      // 얼음 스킬을 날린 마지막시간을 저장할 시간
    SkillManager skillManager;
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
        skillManager = SkillManager.skillInst;
        if (GameObject.FindWithTag("Paladin") != null)
            dialouge = GameObject.FindWithTag("Paladin").GetComponent<NPCPaladinDialouge>();
        else
            dialouge = null;
        mpImage.fillAmount = playerData.MP / playerData.MaxMP;
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
        switch(levelIdx)
        {
            case 0: skillAction[qucikIdx] = Level05SkillCasting; break;
            case 1: skillAction[qucikIdx] = Level10SkillCasting; break;
            case 2: skillAction[qucikIdx] = Level20_01SkillCasting; break;
            case 3: skillAction[qucikIdx] = Level20_02SkillCasting; break;
            case 4: skillAction[qucikIdx] = Level30SkillCasting; break;
        }
    }

    private void Level05SkillCasting()
    {
        if (Time.time - levelLastSkilTime > levelSkillTime[0])
        {   //플레이어가 찍은 레벨스킬로 스킬을 코루틴을 정하고 애니메이션을 실행시킨다
            controller.IsStop(true);
            animator.SetTrigger("Lv05SkillTrigger");
            playerData.MP -= 20;
            mpImage.fillAmount = playerData.MP / playerData.MaxMP;
            Invoke("IsMove", 1.0f);
            levelLastSkilTime = Time.time;
        }
    }
    private void Level10SkillCasting()
    {
        if (Time.time - levelLastSkilTime > levelSkillTime[1])
        {   //플레이어가 찍은 레벨스킬로 스킬을 코루틴을 정하고 애니메이션을 실행시킨다
            controller.IsStop(true);
            animator.SetTrigger("Lv10SkillTrigger");
            playerData.MP -= 50;
            mpImage.fillAmount = playerData.MP / playerData.MaxMP;
            Invoke("IsMove", 1.0f);
            levelLastSkilTime = Time.time;
        }
    
    }
    private void Level20_01SkillCasting()
    {
        if (Time.time - levelLastSkilTime > levelSkillTime[2])
        {   //플레이어가 찍은 레벨스킬로 스킬을 코루틴을 정하고 애니메이션을 실행시킨다
            controller.IsStop(true);
            animator.SetTrigger("Lv20_01SkillTrigger");
            playerData.MP -= 100;
            mpImage.fillAmount = playerData.MP / playerData.MaxMP;
            Invoke("IsMove", 1.0f);
            levelLastSkilTime = Time.time;
        }
    }
    private void Level20_02SkillCasting()
    {
        if (Time.time - levelLastSkilTime > levelSkillTime[3])
        {   //플레이어가 찍은 레벨스킬로 스킬을 코루틴을 정하고 애니메이션을 실행시킨다
            controller.IsStop(true);
            animator.SetTrigger("Lv20_02SkillTrigger");
            playerData.MP -= 100;
            mpImage.fillAmount = playerData.MP / playerData.MaxMP;
            Invoke("IsMove", 1.0f);
            levelLastSkilTime = Time.time;
        }
    }
    private void Level30SkillCasting()
    {
        if (Time.time - levelLastSkilTime > levelSkillTime[5])
        {   //플레이어가 찍은 레벨스킬로 스킬을 코루틴을 정하고 애니메이션을 실행시킨다
            controller.IsStop(true);
            animator.SetTrigger("Lv30SkillTrigger");
            playerData.MP -= 200;
            mpImage.fillAmount = playerData.MP / playerData.MaxMP;
            Invoke("IsMove", 1.0f);
            levelLastSkilTime = Time.time;
        }
    }
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
