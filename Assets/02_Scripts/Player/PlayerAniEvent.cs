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
    [SerializeField] List<GameObject> level20_1EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> level20_2EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> level30EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> slashEffectsList = new List<GameObject>();
    [SerializeField] SkillData lv05SkillData;
    [SerializeField] SkillData lv10SkillData;
    [SerializeField] SkillData lv20_01SkillData;
    [SerializeField] SkillData lv20_02SkillData;
    [SerializeField] SkillData lv30SkillData;
    Transform plTr;
    int skillIdx;
    GameObject attackEff;
    AudioSource audioSource;
    public float attackRadius = 3.0f; // 공격 범위
    SkillState skillState = SkillState.None;
    UnityAction lv05SkillCoroutine;
    UnityAction lv10SkillCoroutine;
    UnityAction lv20_01SkillCoroutine;
    UnityAction lv20_02SkillCoroutine;
    UnityAction lv30SkillCoroutine;
    float critical;
    private void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
        attackEff = slashEffectsList[0];
        plTr = transform.parent;
    }
    public void AttackDown()
    {
        AttackEff(new Vector3(180, 250, 0));
        audioSource.PlayOneShot(playerData.AttackDownClip);
        Attack(playerData.AttackValue, 1.25f, skillState);
    }
    public void AttackUp()
    {
        AttackEff(new Vector3(40, 250, 0));
        audioSource.PlayOneShot(playerData.AttackUpClip);
        Attack(playerData.AttackValue * 1.25f, 1.5f, skillState);
    }
    public void AttackFinish()
    {
        AttackEff(new Vector3(-90, 250, 0));
        audioSource.PlayOneShot(playerData.AttackFinishClip);
        Attack(playerData.AttackValue * 1.5f, 2.0f, skillState);
    }
    public void Lv05MagicSkillCasting() { Debug.Log(lv05SkillCoroutine); lv05SkillCoroutine(); }
    public void Lv10MagicSkillCasting() { lv10SkillCoroutine(); }
    public void Lv20_01MagicSkillCasting() { lv20_01SkillCoroutine(); }
    public void Lv20_02MagicSkillCasting() { lv20_02SkillCoroutine(); }
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
    void IceBallSkill()
    {
        LvelSkill(level10EffectsList, 0, lv10SkillData.IceClip);
    }
    void FireTornado()
    {
        LvelSkill(level10EffectsList, 1, lv10SkillData.FireClip);
        Attack(playerData.MagicAttackValue + 200, 2.0f, SkillState.Fire);
    }
    void ElectroTornado()
    {
        LvelSkill(level10EffectsList, 2, lv10SkillData.ElectroClip);
        Attack(playerData.MagicAttackValue + 200, 2.0f, SkillState.Electro);
    }
    #endregion
    #region Lv20_01스킬
    void IceAuraSkill()
    {
        Auras(0, 1, SkillState.Ice, lv20_01SkillData.IceClip);
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => level20_1EffectsList[0].SetActive(false));
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => IceAuraOn());
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => level20_1EffectsList[0].SetActive(false));
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => IceAuraOn());
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => level20_1EffectsList[0].SetActive(false));
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => IceAuraOn());
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => level20_1EffectsList[0].SetActive(false));
        seq.AppendInterval(1.0f);
        seq.AppendCallback(() => IceAuraOn());
        seq.AppendInterval(2.0f);
        seq.AppendCallback(() =>
        {
            playerData.AttackValue -= 100;
            attackEff = slashEffectsList[0];
            level20_1EffectsList[0].SetActive(false);
            skillState = SkillState.None;
        });
    }

    private void IceAuraOn()
    {
        level20_1EffectsList[0].SetActive(true);
        SoundManager.soundInst.EffectSoundPlay(audioSource, lv20_01SkillData.IceClip);
    }

    void FireAuraSkill()
    {
        Auras(1, 2, SkillState.Fire, lv20_01SkillData.FireClip);
        Invoke("FireAuraOff", 10.0f);
    }

    private void FireAuraOff()
    {
        playerData.AttackValue -= 100;
        attackEff = slashEffectsList[2];
        level20_1EffectsList[1].SetActive(false);
        skillState = SkillState.None;
    }

    void ElectroAuraSkill()
    {
        Auras(2, 3, SkillState.Electro, lv20_01SkillData.ElectroClip);
        Invoke("ElectroAuraOff", 10.0f);
    }

    private void Auras(int effIdx, int slashIdx, SkillState _skillState, AudioClip audioClip)
    {
        level20_1EffectsList[effIdx].SetActive(true);
        attackEff = slashEffectsList[slashIdx];
        playerData.AttackValue += 100;
        skillState = _skillState;
        SoundManager.soundInst.EffectSoundPlay(audioSource, audioClip);
    }

    private void ElectroAuraOff()
    {
        playerData.AttackValue -= 100;
        attackEff = slashEffectsList[3];
        level20_1EffectsList[2].SetActive(false);
        skillState = SkillState.None;
    }
    #endregion
    #region Lv20_02스킬
    void IceStrikeSkill()
    {
        level20_2EffectsList[0].transform.position = plTr.position + Vector3.up;
        level20_2EffectsList[0].transform.rotation = plTr.rotation;
        level20_2EffectsList[0].SetActive(true);
        level20_2EffectsList[0].transform.DOMove(plTr.position + plTr.forward * 20.0f + Vector3.up, 5.0f);
        SoundManager.soundInst.EffectSoundPlay(audioSource, lv20_02SkillData.IceClip);
        Attack(300 + playerData.MagicAttackValue, 5.0f, SkillState.Ice);
    }
    void FireStrikeSkill()
    {
        LvelSkill(level20_2EffectsList, 1, lv20_02SkillData.FireClip);
        Attack(300 + playerData.MagicAttackValue, 5.0f, SkillState.Fire);
    }
    void ElectroStrikeSkill()
    {
        level20_2EffectsList[2].transform.position = plTr.position + Vector3.up;
        level20_2EffectsList[2].transform.rotation = plTr.rotation;
        level20_2EffectsList[2].SetActive(true);
        level20_2EffectsList[2].transform.DOMove(plTr.position + plTr.forward * 20.0f + Vector3.up, 5.0f);
        SoundManager.soundInst.EffectSoundPlay(audioSource, lv20_02SkillData.ElectroClip);
        Attack(300 + playerData.MagicAttackValue, 5.0f, SkillState.Electro);
    }
    #endregion
    #region Lv30스킬

    void IceLaserSkill()
    {
        level30EffectsList[0].transform.position = plTr.position + plTr.forward;
        level30EffectsList[0].transform.rotation = plTr.rotation;
        level30EffectsList[0].SetActive(true);
        SoundManager.soundInst.EffectSoundPlay(audioSource, lv30SkillData.IceClip);
        Attack(500 + playerData.MagicAttackValue, 7.0f, SkillState.Ice);
    }
    void FireMeteorSkill()
    {
        level30EffectsList[1].transform.position = plTr.position + (plTr.forward * 2.0f) + (Vector3.up * 5.0f);
        level30EffectsList[1].transform.rotation = plTr.rotation;
        level30EffectsList[1].SetActive(true);
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            SoundManager.soundInst.EffectSoundPlay(audioSource, lv20_02SkillData.FireClip);
            level30EffectsList[1].transform.DOMove(plTr.position + (plTr.forward * 2.0f), 2.0f);
        });
        seq.AppendInterval(2.0f);
        seq.AppendCallback(() =>
        {
            level30EffectsList[1].transform.GetChild(0).gameObject.SetActive(false);
            SoundManager.soundInst.EffectSoundPlay(audioSource, lv30SkillData.FireClip);
            Attack(500 + playerData.MagicAttackValue, 7.0f, SkillState.Fire);
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
        level30EffectsList[2].transform.position = plTr.position + plTr.forward;
        level30EffectsList[2].transform.rotation = plTr.rotation;
        level30EffectsList[2].SetActive(true);
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            level30EffectsList[2].transform.DOMove(plTr.position + plTr.position, 2.0f);
            SoundManager.soundInst.EffectSoundPlay(audioSource, lv10SkillData.ElectroClip);
        });
        seq.AppendInterval(2.0f);
        seq.AppendCallback(() =>
        {
            level30EffectsList[2].transform.DOMove(plTr.position + plTr.forward * 10, 2.0f);
            SoundManager.soundInst.EffectSoundPlay(audioSource, lv30SkillData.ElectroClip);
            Attack(500 + playerData.MagicAttackValue, 7.0f, SkillState.Electro);
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
        if (chance < playerData.FatalProbability)
        {
            baseDamage *= playerData.FatalValue * 0.01f; // 치명타 발동 시 데미지 증폭
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
                attackEff = level20_1EffectsList[i];
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
                    case SkillState.Ice: lv10SkillCoroutine = IceBallSkill; break;
                    case SkillState.Fire: lv10SkillCoroutine = FireTornado; break;
                    case SkillState.Electro: lv10SkillCoroutine = ElectroTornado; break;
                }
                break;
            case SkillLevelState.Lv20_01:
                switch (state)
                {
                    case SkillState.Ice: lv20_01SkillCoroutine = IceAuraSkill; break;
                    case SkillState.Fire: lv20_01SkillCoroutine = FireAuraSkill; break;
                    case SkillState.Electro: lv20_01SkillCoroutine = ElectroAuraSkill; break;
                }
                break;
            case SkillLevelState.Lv20_02:
                switch (state)
                {
                    case SkillState.Ice: lv20_02SkillCoroutine = IceStrikeSkill; break;
                    case SkillState.Fire: lv20_02SkillCoroutine = FireStrikeSkill; break;
                    case SkillState.Electro: lv20_02SkillCoroutine = ElectroStrikeSkill; break;
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
