using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 스킬 만들자
public class PlayerAniEvent : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] List<GameObject> level05EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> level10EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> level20_1EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> level20_2EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> level30EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> slashEffectsList = new List<GameObject>();
    Transform plTr;
    int skillIdx;
    GameObject attackEff;
    AudioSource audioSource;
    public float attackRadius = 3.0f; // 공격 범위
    IEnumerator lv05SkillCoroutine;
    IEnumerator lv10SkillCoroutine;
    IEnumerator lv20_01SkillCoroutine;
    IEnumerator lv20_02SkillCoroutine;
    IEnumerator lv30SkillCoroutine;
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
        Attack(1, 1.25f);
    }
    public void AttackUp()
    {
        AttackEff(new Vector3(40, 250, 0));
        audioSource.PlayOneShot(playerData.AttackUpClip);
        Attack(1.25f, 1.5f);
    }
    public void AttackFinish()
    {
        AttackEff(new Vector3(-90, 250, 0));
        audioSource.PlayOneShot(playerData.AttackFinishClip);
        Attack(1.5f, 2.0f);
    }
    public void Lv05MagicSkillCasting() { StartCoroutine(lv05SkillCoroutine); }
    public void Lv10MagicSkillCasting() { StartCoroutine(lv10SkillCoroutine); }
    public void Lv20_01MagicSkillCasting() { StartCoroutine(lv20_01SkillCoroutine); }
    public void Lv20_02MagicSkillCasting() { StartCoroutine(lv20_02SkillCoroutine); }
    public void Lv30MagicSkillCasting() { StartCoroutine(lv30SkillCoroutine); }
    #region Lv05스킬
    IEnumerator IceSpearSkill()
    {
        level05EffectsList[0].transform.position = plTr.position;
        level05EffectsList[0].transform.rotation = plTr.rotation;
        level05EffectsList[0].SetActive(true);
        yield return new WaitForSeconds(0.3f);
        level05EffectsList[3].transform.position = plTr.position + Vector3.up + Vector3.forward;
        level05EffectsList[3].transform.rotation = plTr.rotation;
        level05EffectsList[3].SetActive(true);
    }
    IEnumerator FireBallSkill()
    {
        level05EffectsList[1].transform.position = plTr.position;
        level05EffectsList[1].transform.rotation = plTr.rotation;
        level05EffectsList[1].SetActive(true);
        yield return null;
    }
    IEnumerator EelectroBallSkill()
    {
        level05EffectsList[2].transform.position = plTr.position;
        level05EffectsList[2].transform.rotation = plTr.rotation;
        level05EffectsList[2].SetActive(true);
        yield return null;
    }
    #endregion
    #region Lv10스킬
    IEnumerator IceBallSkill()
    {
        level10EffectsList[0].transform.position = plTr.position;
        level10EffectsList[0].transform.rotation = plTr.rotation;
        level10EffectsList[0].SetActive(true);
        yield return null;
    }
    IEnumerator FireTornado()
    {
        level10EffectsList[1].transform.position = plTr.position;
        level10EffectsList[1].transform.rotation = plTr.rotation;
        level10EffectsList[1].SetActive(true);
        yield return null;
    }
    IEnumerator ElectroTornado()
    {
        level10EffectsList[2].transform.position = plTr.position;
        level10EffectsList[2].transform.rotation = plTr.rotation;
        level10EffectsList[2].SetActive(true);
        yield return null;
    }
    #endregion
    #region Lv20_01스킬
    IEnumerator IceAuraSkill()
    {
        level20_1EffectsList[0].SetActive(true);
        yield return null;
    }
    IEnumerator FireAuraSkill()
    {
        level20_1EffectsList[1].SetActive(true);
        yield return null;
    }
    IEnumerator ElectroAuraSkill()
    {
        level20_1EffectsList[2].SetActive(true);
        yield return null;
    }
    #endregion
    #region Lv20_02스킬
    IEnumerator IceStrikeSkill()
    {
        level20_2EffectsList[0].transform.position = plTr.position;
        level20_2EffectsList[0].transform.rotation = plTr.rotation;
        level20_2EffectsList[0].SetActive(true);
        yield return null;
    }
    IEnumerator FireStrikeSkill()
    {
        level20_2EffectsList[1].transform.position = plTr.position;
        level20_2EffectsList[1].transform.rotation = plTr.rotation;
        level20_2EffectsList[1].SetActive(true);
        yield return null;
    }
    IEnumerator ElectroStrikeSkill()
    {
        level20_2EffectsList[2].transform.position = plTr.position;
        level20_2EffectsList[2].transform.rotation = plTr.rotation;
        level20_2EffectsList[2].SetActive(true);
        yield return null;
    }
    #endregion
    #region Lv30스킬

    IEnumerator IceLaserSkill()
    {
        level20_2EffectsList[0].transform.position = plTr.position;
        level20_2EffectsList[0].transform.rotation = plTr.rotation;
        level20_2EffectsList[0].SetActive(true);
        yield return null;
    }
    IEnumerator FireMeteorSkill()
    {
        level20_2EffectsList[1].transform.position = plTr.position;
        level20_2EffectsList[1].transform.rotation = plTr.rotation;
        level20_2EffectsList[1].SetActive(true);
        yield return null;
    }
    IEnumerator ElectroArrowSkill()
    {
        level20_2EffectsList[2].transform.position = plTr.position;
        level20_2EffectsList[2].transform.rotation = plTr.rotation;
        level20_2EffectsList[2].SetActive(true);
        yield return null;
    }
    #endregion
    private void AttackEff(Vector3 rot)
    {
        attackEff.transform.localEulerAngles = rot;
        attackEff.SetActive(true);
    }
    private void Attack(float damage, float radius)
    {
        // 스켈레톤의 위치에서 attackRadius 반경으로 오버랩 스피어 생성
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius * radius);

        // 기본 공격력 설정
        float baseDamage = playerData.AttackValue * damage;

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
                skeletonDamage.MutantHit(baseDamage);
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
                    case SkillState.Ice: lv05SkillCoroutine = IceSpearSkill(); break;
                    case SkillState.Fire: lv05SkillCoroutine = FireBallSkill(); break;
                    case SkillState.Electro: lv05SkillCoroutine = EelectroBallSkill(); break;
                }
                break;
            case SkillLevelState.Lv10:
                switch (state)
                {
                    case SkillState.Ice: lv10SkillCoroutine = IceBallSkill(); break;
                    case SkillState.Fire: lv10SkillCoroutine = FireTornado(); break;
                    case SkillState.Electro: lv10SkillCoroutine = ElectroTornado(); break;
                }
                break;
            case SkillLevelState.Lv20_01:
                switch (state)
                {
                    case SkillState.Ice: lv20_01SkillCoroutine = IceAuraSkill(); break;
                    case SkillState.Fire: lv20_01SkillCoroutine = FireAuraSkill(); break;
                    case SkillState.Electro: lv20_01SkillCoroutine = ElectroAuraSkill(); break;
                }
                break;
            case SkillLevelState.Lv20_02:
                switch (state)
                {
                    case SkillState.Ice: lv20_02SkillCoroutine = IceStrikeSkill(); break;
                    case SkillState.Fire: lv20_02SkillCoroutine = FireStrikeSkill(); break;
                    case SkillState.Electro: lv20_02SkillCoroutine = ElectroStrikeSkill(); break;
                }
                break;
            case SkillLevelState.Lv30:
                switch (state)
                {
                    case SkillState.Ice: lv30SkillCoroutine = IceLaserSkill(); break;
                    case SkillState.Fire: lv30SkillCoroutine = FireMeteorSkill(); break;
                    case SkillState.Electro: lv30SkillCoroutine = ElectroArrowSkill(); break;
                }
                break;
        }
        
    }
}
