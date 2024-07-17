using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniEvent : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] List<GameObject> level05EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> level10EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> level15_1EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> level15_2EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> level25EffectsList = new List<GameObject>();
    [SerializeField] List<GameObject> slashEffectsList = new List<GameObject>();
    Transform paTr;
    int skillIdx;
    GameObject attackEff;
    AudioSource audioSource;
    public float attackRadius = 3.0f; // ���� ����
    IEnumerator skillCoroutine;
    float critical;
    private void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
        skillCoroutine = IceSpearSkill();
        attackEff = slashEffectsList[0];
        paTr = transform.parent;
    }
    public void AttackDown()
    {
        AttackEff(new Vector3(180, 250,0));
        audioSource.PlayOneShot(playerData.AttackDownClip);
        Attack(1);
    }
    public void AttackUp()
    {
        AttackEff(new Vector3(40, 250, 0));
        audioSource.PlayOneShot(playerData.AttackUpClip);
        Attack(1.5f);
    }
    public void AttackFinish()
    {
        AttackEff(new Vector3(-90, 250, 0));
        audioSource.PlayOneShot(playerData.AttackFinishClip);
        Attack(2.0f);
    }
    public void IceSkillCasting()
    {
        StartCoroutine(skillCoroutine);
    }
    IEnumerator IceSpearSkill()
    {
        level05EffectsList[0].transform.position = paTr.position;
        level05EffectsList[0].transform.rotation = paTr.rotation;
        level05EffectsList[0].SetActive(true);
        yield return new WaitForSeconds(0.3f);
        level05EffectsList[3].transform.position = paTr.position + Vector3.up + Vector3.forward;
        level05EffectsList[3].transform.rotation = paTr.rotation;
        level05EffectsList[3].SetActive(true);
        yield return new WaitForSeconds(0.6f);
        level05EffectsList[0].SetActive(false);
    }
    IEnumerator FireBallSkill()
    {
        level05EffectsList[1].transform.position = paTr.position;
        level05EffectsList[1].transform.rotation = paTr.rotation;
        level05EffectsList[1].SetActive(true);
        yield return new WaitForSeconds(3.0f);
        level05EffectsList[1].SetActive(false);
    }
    IEnumerator EelectroBallSkill()
    {
        level05EffectsList[2].transform.position = paTr.position;
        level05EffectsList[2].transform.rotation = paTr.rotation;
        level05EffectsList[2].SetActive(true);
        yield return new WaitForSeconds(3.0f);
        level05EffectsList[2].SetActive(false);
    }
    private void AttackEff(Vector3 rot)
    {
        attackEff.transform.localEulerAngles = rot;
        attackEff.SetActive(true);
    }
    private void Attack(float damage)
    {
        // ���̷����� ��ġ���� attackRadius �ݰ����� ������ ���Ǿ� ����
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);

        // �⺻ ���ݷ� ����
        float baseDamage = playerData.AttackValue;

        // ġ��Ÿ Ȯ�� üũ
        System.Random random = new System.Random();
        float chance = (float)random.NextDouble();
        if (chance < playerData.FatalProbability)
        {
            baseDamage *= playerData.FatalValue; // ġ��Ÿ �ߵ� �� ������ ����
            Debug.Log(baseDamage);
        }

        // ������ ���Ǿ� ���� ��� �ݶ��̴� Ȯ��
        foreach (var hitCollider in hitColliders)
        {
            // AttackValue ��ũ��Ʈ�� ���� ��ü���� Ȯ��
            MutantDamage skeletonDamage = hitCollider.GetComponent<MutantDamage>();
            if (skeletonDamage != null)
            {
                skeletonDamage.SkeletonHit(baseDamage);
                Debug.Log(baseDamage);
            }
        }
    }
    public void ChangeAttackEff(int level15_1Idx)
    {
        for(int i = 0; i < 3 ; i++)
        {
            if (level15_1Idx == i)
                attackEff = level15_1EffectsList[i];
        } 
    }
    public void Level05SkillChange(int skiiIdx)
    {
        if (skiiIdx == 1)
            skillCoroutine = IceSpearSkill();
        else if (skiiIdx == 2)
            skillCoroutine = FireBallSkill();
        else if (skiiIdx == 3)
            skillCoroutine = EelectroBallSkill();
    }
}
