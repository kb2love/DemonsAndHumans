using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MutantBoss : MonoBehaviour
{
    [SerializeField] GameObject magicShield;
    [SerializeField] GameObject jumpAttackEff;
    [SerializeField] GameObject areaAttackEff;
    [SerializeField] float attackRadius = 5.0f;
    [SerializeField] float damage = 50.0f;
    MutantAI mutantAI;
    MutantDamage mutantDamage;
    private void Start()
    {
        mutantDamage = GetComponent<MutantDamage>();
        mutantAI = GetComponent<MutantAI>();
    }
    public void MagicShield()
    { 
        magicShield.SetActive(true);
        mutantDamage.IsMutantShield(true);
        Invoke("ShieldOff", 3.5f);
    }
    public void JumpAttack()
    {
        jumpAttackEff.SetActive(true);
        Attack(1.5f, 2.0f);
        Invoke("JumpAtkEffOff", 2.0f);
    }
    public void AreaAttack()
    {
        areaAttackEff.SetActive(true);
        Attack(2.0f, 1.5f);
        Invoke("AreaAtkEffOff", 2.0f);
    }
    void Attack(float radiusValue, float damageValue)
    {
        // 스켈레톤의 위치에서 attackRadius 반경으로 오버랩 스피어 생성
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius * radiusValue);

        // 오버랩 스피어 내의 모든 콜라이더 확인
        foreach (var hitCollider in hitColliders)
        {
            // AttackValue 스크립트를 가진 객체인지 확인
            PlayerDamage playerDamage = hitCollider.GetComponent<PlayerDamage>();
            if (playerDamage != null)
            {
                playerDamage.HitDamage(damage * damageValue);
            }
        }
    }
    void ShieldOff() { magicShield.SetActive(false); mutantDamage.IsMutantShield(false); }
    void JumpAtkEffOff() { jumpAttackEff.SetActive(false);}
    void AreaAtkEffOff() {  areaAttackEff.SetActive(false);}
}
