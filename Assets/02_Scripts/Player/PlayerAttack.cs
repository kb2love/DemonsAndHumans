
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] QuestData03 questData;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject shield;
    [SerializeField] Image mpImage;
    NPCPaladinDialouge dialouge;
    Animator animator;
    PlayerController controller;
    PlayerDamage plDamage;
    PlayerAniEvent aniEvent;
    private int attackCount = 0;
    private float comboResetTime = 1.0f; // 연속 공격을 초기화하는 시간
    private float stopAttackTime = 1.25f; // 시간이 지나면 공격카운트를 초기화할 시간
    //어택 시간
    private float lastAttackTime;       // 현재시간을 저장할 공격한 시간
    private float stopLastAttackTime;   // 현재시간을 저장할 공격을 멈출 시간
    //스킬 시간
    [SerializeField] private float level5SkillTime = 10.0f; // 스킬 쿨타임
    private float level5LastSkilTime;      // 얼음 스킬을 날린 마지막시간을 저장할 시간
    SkillManager skillManager;
    private bool isStop;
    private bool isEquip;
    private bool isInven;
    public bool isSword
    {
        get { return _isSword; }
    }
    private bool _isSword;
    public bool isShield
    {
        get { return _isShield; }
    }
    private bool _isShield;
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

    // Update is called once per frame
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
        if (Input.GetKeyDown(KeyCode.Q) && Level05(0))
        {
            Level05SkillCasting();
        }
        else if (Input.GetKeyDown(KeyCode.E) && Level05(1))
        {
            Level05SkillCasting();
        }
        else if (Input.GetKeyDown(KeyCode.R) && Level05(2))
        {
            Level05SkillCasting();
        }

    }

    private void Level05SkillCasting()
    {
        for (int i = 1; i < 4; i++)
        {
            if (playerData.level05SkillIdx == i && Time.time - level5LastSkilTime > level5SkillTime)
            {
                controller.IsStop(true);
                aniEvent.Level05SkillChange(i);
                animator.SetTrigger("CastingTrigger");
                playerData.MP -= 30;
                mpImage.fillAmount = playerData.MP / playerData.MaxMP;
                Invoke("IsMove", 1.0f);
                level5LastSkilTime = Time.time;
                break;
            }
        }
    }

    bool Level05(int skillListIdx)
    {
        return skillManager.skillList[skillListIdx].sprite == skillManager.skillImageList[playerData.level05SkillIdx - 1];
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
        _isSword = _IsSword;
        animator.SetBool("GetSword", isSword);
        sword.SetActive(isSword);
        if (isSword)
        {
            questData.Result = true;
            if (dialouge != null)
                dialouge.WeaponWear();
        }
    }
    public void IsShield(bool _IsShield)
    {
        _isShield = _IsShield;
        animator.SetBool("GetShield", isShield);
        shield.SetActive(isShield);
    }
}
