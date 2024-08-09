using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class PlayerAniEvent : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] List<GameObject> level05EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> level10EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> level20EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> level25EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> level30EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> slashEffectsList = new List<GameObject>();
    [SerializeField] SkillData lv05SkillData;
    [SerializeField] SkillData lv10SkillData;
    [SerializeField] SkillData lv20SkillData;
    [SerializeField] SkillData lv25SkillData;
    [SerializeField] SkillData lv30SkillData;
    [SerializeField] AudioClip attackDownClip;
    [SerializeField] AudioClip attackUpClip;
    [SerializeField] AudioClip attackFinishClip;
    AudioClip saveAttackDownClip;
    AudioClip saveAttackUpClip;
    AudioClip saveAttackFinishClip;
    Transform plTr;
    int skillIdx;
    GameObject attackEff;
    AudioSource audioSource;
    public float attackRadius = 3.0f; // 공격 범위
    SkillState skillState = SkillState.None;
    UnityAction lv05SkillCoroutine;
    UnityAction lv10SkillCoroutine;
    UnityAction lv20SkillCoroutine;
    UnityAction lv25SkillCoroutine;
    UnityAction lv30SkillCoroutine;
    float critical;
    GameManager GM;
    private void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
        attackEff = slashEffectsList[0];
        saveAttackDownClip = attackDownClip;
        saveAttackUpClip = attackUpClip;
        saveAttackFinishClip = attackFinishClip;
        plTr = transform.parent;
        GM = GameManager.GM;
    }
    public void AttackDown()
    {
        AttackEff(new Vector3(180, 250, 0));
        SoundManager.soundInst.EffectSoundPlay(audioSource, saveAttackDownClip);
        Attack(GM.playerDataJson.AttackValue, 1.25f, skillState);
    }
    public void AttackUp()
    {
        AttackEff(new Vector3(40, 250, 0));
        SoundManager.soundInst.EffectSoundPlay(audioSource, saveAttackUpClip);
        Attack(GM.playerDataJson.AttackValue * 1.25f, 1.5f, skillState);
    }
    public void AttackFinish()
    {
        AttackEff(new Vector3(-90, 250, 0));
        SoundManager.soundInst.EffectSoundPlay(audioSource, saveAttackFinishClip);
        Attack(GM.playerDataJson.AttackValue * 1.5f, 2.0f, skillState);
    }
    public void Lv05MagicSkillCasting() { lv05SkillCoroutine(); }
    public void Lv10MagicSkillCasting() { lv10SkillCoroutine(); }
    public void Lv20MagicSkillCasting() { lv20SkillCoroutine(); }
    public void Lv25MagicSkillCasting() { lv25SkillCoroutine(); }
    public void Lv30MagicSkillCasting() { lv30SkillCoroutine(); }
    #region Lv05스킬
    void IceSpearSkill()
    {
        LvelSkill(level05EffectsList, 0, lv05SkillData.IceClip);
        Invoke("IceShoot", 0.3f);
    }
    private void IceShoot()
    {
        level05EffectsList[3].transform.position = plTr.position + Vector3.up + Vector3.forward;
        level05EffectsList[3].transform.rotation = plTr.rotation;
        level05EffectsList[3].SetActive(true);
    }

    void FireBallSkill() { LvelSkill(level05EffectsList, 1, lv05SkillData.FireClip); }
    void EelectroBallSkill() { LvelSkill(level05EffectsList, 2, lv05SkillData.ElectroClip); }
    #endregion
    #region Lv10스킬
    void IceTornadoSkill()
    {
        LvelSkill(level20EffectsList, 0, lv10SkillData.IceClip);
        Attack(GM.playerDataJson.MagicAttackValue + 200, 2.0f, SkillState.Ice);
        Invoke("IceTornadoOff", 1.5f);
    }
    void IceTornadoOff() { level20EffectsList[0].gameObject.SetActive(false); }
    void FireTornado()
    {
        LvelSkill(level10EffectsList, 1, lv10SkillData.FireClip);
        Attack(GM.playerDataJson.MagicAttackValue + 200, 2.0f, SkillState.Fire);
    }
    void ElectroTornado()
    {
        LvelSkill(level10EffectsList, 2, lv10SkillData.ElectroClip);
        Attack(GM.playerDataJson.MagicAttackValue + 200, 2.0f, SkillState.Electro);
    }
    #endregion
    #region Lv20스킬
    void IceAuraSkill()
    {
        Auras(0, 1, SkillState.Ice, lv20SkillData.IceClip, lv05SkillData.IceClip);
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => level20EffectsList[0].SetActive(false));
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => IceAuraOn());
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => level20EffectsList[0].SetActive(false));
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => IceAuraOn());
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => level20EffectsList[0].SetActive(false));
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => IceAuraOn());
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => level20EffectsList[0].SetActive(false));
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => IceAuraOn());
        seq.AppendInterval(2.0f);
        seq.AppendCallback(() =>
        {
            GM.playerDataJson.AttackValue -= 100;
            attackEff = slashEffectsList[0];
            level20EffectsList[0].SetActive(false);
            skillState = SkillState.None;
            saveAttackDownClip = attackDownClip;
            saveAttackUpClip = attackUpClip;
            saveAttackFinishClip = attackFinishClip;
        });
    }

    private void IceAuraOn()
    {
        level20EffectsList[0].SetActive(true);
        SoundManager.soundInst.EffectSoundPlay(audioSource, lv20SkillData.IceClip);
    }

    void FireAuraSkill()
    {
        Auras(1, 2, SkillState.Fire, lv20SkillData.FireClip, lv05SkillData.FireClip);
        Invoke("FireAuraOff", 10.0f);
    }

    private void FireAuraOff()
    {
        GM.playerDataJson.AttackValue -= 100;
        attackEff = slashEffectsList[0];
        level20EffectsList[1].SetActive(false);
        skillState = SkillState.None;
        saveAttackDownClip = attackDownClip;
        saveAttackUpClip = attackUpClip;
        saveAttackFinishClip = attackFinishClip;
    }

    void ElectroAuraSkill()
    {
        Auras(2, 3, SkillState.Electro, lv20SkillData.ElectroClip, lv25SkillData.ElectroClip);
        Invoke("ElectroAuraOff", 10.0f);
    }

    private void Auras(int effIdx, int slashIdx, SkillState _skillState, AudioClip audioClip, AudioClip clip)
    {
        level20EffectsList[effIdx].SetActive(true);
        attackEff = slashEffectsList[slashIdx];
        GM.playerDataJson.AttackValue += 100;
        skillState = _skillState;
        SoundManager.soundInst.EffectSoundPlay(audioSource, audioClip);
        saveAttackDownClip = clip;
        saveAttackUpClip = clip;
        saveAttackFinishClip = clip;
    }

    private void ElectroAuraOff()
    {
        GM.playerDataJson.AttackValue -= 100;
        attackEff = slashEffectsList[0];
        level20EffectsList[2].SetActive(false);
        skillState = SkillState.None;
        saveAttackDownClip = attackDownClip;
        saveAttackUpClip = attackUpClip;
        saveAttackFinishClip = attackFinishClip;
    }
    #endregion
    #region Lv25스킬
    void IceStrikeSkill()
    {
        level25EffectsList[0].transform.position = plTr.position + Vector3.up;
        level25EffectsList[0].transform.rotation = plTr.rotation;
        level25EffectsList[0].SetActive(true);
        level25EffectsList[0].transform.DOMove(plTr.position + plTr.forward * 20.0f + Vector3.up, 5.0f);
        SoundManager.soundInst.EffectSoundPlay(audioSource, lv25SkillData.IceClip);
        Attack(300 + GM.playerDataJson.MagicAttackValue, 5.0f, SkillState.Ice);
    }
    void FireStrikeSkill()
    {
        LvelSkill(level25EffectsList, 1, lv25SkillData.FireClip);
        Attack(300 + GM.playerDataJson.MagicAttackValue, 5.0f, SkillState.Fire);
    }
    void ElectroStrikeSkill()
    {
        level25EffectsList[2].transform.position = plTr.position + Vector3.up;
        level25EffectsList[2].transform.rotation = plTr.rotation;
        level25EffectsList[2].SetActive(true);
        level25EffectsList[2].transform.DOMove(plTr.position + plTr.forward * 20.0f + Vector3.up, 5.0f);
        SoundManager.soundInst.EffectSoundPlay(audioSource, lv25SkillData.ElectroClip);
        Attack(300 + GM.playerDataJson.MagicAttackValue, 5.0f, SkillState.Electro);
    }
    #endregion
    #region Lv30스킬

    void IceLaserSkill()
    {
        level30EffectsList[0].transform.position = plTr.position + plTr.forward;
        level30EffectsList[0].transform.rotation = plTr.rotation;
        level30EffectsList[0].SetActive(true);
        SoundManager.soundInst.EffectSoundPlay(audioSource, lv30SkillData.IceClip);
        Attack(500 + GM.playerDataJson.MagicAttackValue, 7.0f, SkillState.Ice);
    }
    void FireMeteorSkill()
    {
        level30EffectsList[1].transform.position = plTr.position + (plTr.forward * 2.0f) + (Vector3.up * 5.0f);
        level30EffectsList[1].transform.rotation = plTr.rotation;
        level30EffectsList[1].SetActive(true);
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            SoundManager.soundInst.EffectSoundPlay(audioSource, lv25SkillData.FireClip);
            level30EffectsList[1].transform.DOMove(plTr.position + (plTr.forward * 2.0f), 2.0f);
        });
        seq.AppendInterval(2.0f);
        seq.AppendCallback(() =>
        {
            level30EffectsList[1].transform.GetChild(0).gameObject.SetActive(false);
            SoundManager.soundInst.EffectSoundPlay(audioSource, lv30SkillData.FireClip);
            Attack(500 + GM.playerDataJson.MagicAttackValue, 7.0f, SkillState.Fire);
            level30EffectsList[3].gameObject.SetActive(true);
        });
        seq.AppendInterval(2.0f);
        seq.AppendCallback(() =>
        {
            level30EffectsList[1].SetActive(false);
            level30EffectsList[1].transform.GetChild(0).gameObject.SetActive(true);
            level30EffectsList[3].gameObject.SetActive(false);
        });
    }
    void ElectroArrowSkill()
    {
        level30EffectsList[2].transform.position = plTr.position;
        level30EffectsList[2].transform.rotation = plTr.rotation;
        level30EffectsList[2].SetActive(true);
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            level30EffectsList[2].transform.DOMove(plTr.position + plTr.forward, 2.0f);
            SoundManager.soundInst.EffectSoundPlay(audioSource, lv10SkillData.ElectroClip);
        });
        seq.AppendInterval(2.0f);
        seq.AppendCallback(() =>
        {
            level30EffectsList[2].transform.DOMove(plTr.position + plTr.forward * 10, 2.0f);
            SoundManager.soundInst.EffectSoundPlay(audioSource, lv30SkillData.ElectroClip);
            Attack(500 + GM.playerDataJson.MagicAttackValue, 7.0f, SkillState.Electro);
        });
        seq.AppendInterval(2.0f);
        seq.AppendCallback(() => level30EffectsList[2].SetActive(false));

    }
    private void LvelSkill(List<GameObject> effList, int effIdx, AudioClip clip)
    {
        effList[effIdx].transform.position = plTr.position;
        effList[effIdx].transform.rotation = plTr.rotation;
        effList[effIdx].SetActive(true);
        SoundManager.soundInst.EffectSoundPlay(audioSource, clip);
    }
    #endregion
    private void AttackEff(Vector3 rot)
    {
        attackEff.transform.localEulerAngles = rot;
        attackEff.SetActive(true);
    }
    private void Attack(float damage, float radius, SkillState skillState)
    {
        // 스켈레톤의 위치에서 attackRadius 반경으로 오버랩 스피어 생성
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius * radius);

        // 기본 공격력 설정
        float baseDamage = damage;

        // 치명타 확률 체크
        System.Random random = new System.Random();
        float chance = (float)random.NextDouble();
        if (chance < GM.playerDataJson.FatalProbability)
        {
            baseDamage *= GM.playerDataJson.FatalAttackValue * 0.01f; // 치명타 발동 시 데미지 증폭
        }

        // 오버랩 스피어 내의 모든 콜라이더 확인
        foreach (var hitCollider in hitColliders)
        {
            // AttackValue 스크립트를 가진 객체인지 확인
            MutantDamage skeletonDamage = hitCollider.GetComponent<MutantDamage>();
            if (skeletonDamage != null)
            {
                skeletonDamage.MutantHit(baseDamage, skillState);
            }
        }
    }
    public void ChangeAttackEff(int level15_1Idx)
    {
        for (int i = 0; i < 3; i++)
        {
            if (level15_1Idx == i)
                attackEff = level20EffectsList[i];
        }
    }
    public void LevelSkillChange(SkillState state, SkillLevelState level)
    {// 스킬매니저에서 스킬을 교체하는 메서드
        switch (level)
        {
            case SkillLevelState.Lv05:
                switch (state)
                {
                    case SkillState.Ice: lv05SkillCoroutine = IceSpearSkill; break;
                    case SkillState.Fire: lv05SkillCoroutine = FireBallSkill; break;
                    case SkillState.Electro: lv05SkillCoroutine = EelectroBallSkill; break;
                }
                break;
            case SkillLevelState.Lv10:
                switch (state)
                {
                    case SkillState.Ice: lv10SkillCoroutine = IceTornadoSkill; break;
                    case SkillState.Fire: lv10SkillCoroutine = FireTornado; break;
                    case SkillState.Electro: lv10SkillCoroutine = ElectroTornado; break;
                }
                break;
            case SkillLevelState.Lv20:
                switch (state)
                {
                    case SkillState.Ice: lv20SkillCoroutine = IceAuraSkill; break;
                    case SkillState.Fire: lv20SkillCoroutine = FireAuraSkill; break;
                    case SkillState.Electro: lv20SkillCoroutine = ElectroAuraSkill; break;
                }
                break;
            case SkillLevelState.Lv25:
                switch (state)
                {
                    case SkillState.Ice: lv25SkillCoroutine = IceStrikeSkill; break;
                    case SkillState.Fire: lv25SkillCoroutine = FireStrikeSkill; break;
                    case SkillState.Electro: lv25SkillCoroutine = ElectroStrikeSkill; break;
                }
                break;
            case SkillLevelState.Lv30:
                switch (state)
                {
                    case SkillState.Ice: lv30SkillCoroutine = IceLaserSkill; break;
                    case SkillState.Fire: lv30SkillCoroutine = FireMeteorSkill; break;
                    case SkillState.Electro: lv30SkillCoroutine = ElectroArrowSkill; break;
                }
                break;
        }

    }
}
