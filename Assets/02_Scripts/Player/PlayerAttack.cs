
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
    private float comboResetTime = 1.0f; // ���� ������ �ʱ�ȭ�ϴ� �ð�
    private float stopAttackTime = 1.25f; // �ð��� ������ ����ī��Ʈ�� �ʱ�ȭ�� �ð�
    private float lastAttackTime;       // ����ð��� ������ ������ �ð�
    private float stopLastAttackTime;   // ����ð��� ������ ������ ���� �ð�
    [SerializeField] private float level5SkillTime = 10.0f; // ��ų ��Ÿ��
    private float level5LastSkilTime;      // ���� ��ų�� ���� �������ð��� ������ �ð�
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
        // ��ų �ε����� �����ϴ� ���� �޼���
        /*int GetSkillIndex(SkillState state, int baseIndex)
        {
            switch (state)
            {
                case SkillState.Ice:
                    return baseIndex;
                case SkillState.Fire:
                    return baseIndex + 1;
                case SkillState.Electro:
                    return baseIndex + 2;
                default:
                    return -1;
            }
        }*/

        // �� ������ ���� ��ų �ε��� ����
        /*int[] skillIndices = new int[5];
        skillIndices[0] = GetSkillIndex(playerData.level05State, 0);
        skillIndices[1] = GetSkillIndex(playerData.level10State, 3);
        skillIndices[2] = GetSkillIndex(playerData.level20_01State, 6);
        skillIndices[3] = GetSkillIndex(playerData.level20_02State, 9);
        skillIndices[4] = GetSkillIndex(playerData.level30State, 12);*/

        // Ű �Է¿� ���� ��ų ����
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
        if (Time.time - level5LastSkilTime > level5SkillTime)
        {   //�÷��̾ ���� ������ų�� ��ų�� �ڷ�ƾ�� ���ϰ� �ִϸ��̼��� �����Ų��
            controller.IsStop(true);
            animator.SetTrigger("Lv05SkillTrigger");
            playerData.MP -= 30;
            mpImage.fillAmount = playerData.MP / playerData.MaxMP;
            Invoke("IsMove", 1.0f);
            level5LastSkilTime = Time.time;
        }
    }
    private void Level10SkillCasting()
    {
        if (Time.time - level5LastSkilTime > level5SkillTime)
        {   //�÷��̾ ���� ������ų�� ��ų�� �ڷ�ƾ�� ���ϰ� �ִϸ��̼��� �����Ų��
            controller.IsStop(true);
            animator.SetTrigger("Lv10SkillTrigger");
            playerData.MP -= 60;
            mpImage.fillAmount = playerData.MP / playerData.MaxMP;
            Invoke("IsMove", 1.0f);
            level5LastSkilTime = Time.time;
        }
    
    }
    private void Level20_01SkillCasting()
    {
        if (Time.time - level5LastSkilTime > level5SkillTime)
        {   //�÷��̾ ���� ������ų�� ��ų�� �ڷ�ƾ�� ���ϰ� �ִϸ��̼��� �����Ų��
            controller.IsStop(true);
            animator.SetTrigger("Lv20_01SkillTrigger");
            playerData.MP -= 120;
            mpImage.fillAmount = playerData.MP / playerData.MaxMP;
            Invoke("IsMove", 1.0f);
            level5LastSkilTime = Time.time;
        }
    }
    private void Level20_02SkillCasting()
    {
        if (Time.time - level5LastSkilTime > level5SkillTime)
        {   //�÷��̾ ���� ������ų�� ��ų�� �ڷ�ƾ�� ���ϰ� �ִϸ��̼��� �����Ų��
            controller.IsStop(true);
            animator.SetTrigger("Lv20_02SkillTrigger");
            playerData.MP -= 120;
            mpImage.fillAmount = playerData.MP / playerData.MaxMP;
            Invoke("IsMove", 1.0f);
            level5LastSkilTime = Time.time;
        }
    }
    private void Level30SkillCasting()
    {
        if (Time.time - level5LastSkilTime > level5SkillTime)
        {   //�÷��̾ ���� ������ų�� ��ų�� �ڷ�ƾ�� ���ϰ� �ִϸ��̼��� �����Ų��
            controller.IsStop(true);
            animator.SetTrigger("Lv30SkillTrigger");
            playerData.MP -= 300;
            mpImage.fillAmount = playerData.MP / playerData.MaxMP;
            Invoke("IsMove", 1.0f);
            level5LastSkilTime = Time.time;
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
