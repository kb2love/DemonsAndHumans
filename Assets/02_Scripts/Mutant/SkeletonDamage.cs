using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonDamage : MonoBehaviour
{
    [SerializeField] MutantData mutantData;
    [SerializeField] Image hpImage;
    SkeletonAI skeletonAI;
    Animator animator;
    GameObject hitEff;
    float hp;
    private void Start()
    {
        hp = mutantData.skMaxHp;
        skeletonAI = GetComponent<SkeletonAI>();
        animator = GetComponent<Animator>();
    }
    public void SkeletonHit(float damage)
    {
        hp -= damage;
        hpImage.fillAmount = hp / mutantData.skMaxHp;
        hitEff = ObjectPoolingManager.objInst.GetHitEff();
        hitEff.transform.position = transform.position + (Vector3.up * 0.8f);
        hitEff.SetActive(true);
        skeletonAI.IsHit(true);
        animator.SetTrigger("HitTrigger");
        Debug.Log(hp);
        if (hp <= 0)
            SkeletonDie();
    }
    void SkeletonDie()
    {
        skeletonAI.IsDie(true);
        GameManager.GM.ExpUp(50);

    }
    void GenerateLoot()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < 90f)
        {
            // 90% Ȯ���� gold ����
            Instantiate(mutantData.gold, transform.position, Quaternion.identity);
        }
        else if (randomValue < 98f)
        {
            // 8% Ȯ���� normalItem ���� (90% + 8%)
            Instantiate(mutantData.normalItem, transform.position, Quaternion.identity);
        }
        else if (randomValue < 100f)
        {
            // 2% Ȯ���� equipmentItem ���� (90% + 8% + 2%)
            Instantiate(mutantData.equipmentItem, transform.position, Quaternion.identity);
        }
    }
}
