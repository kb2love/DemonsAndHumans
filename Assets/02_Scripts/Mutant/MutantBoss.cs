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
        // ���̷����� ��ġ���� attackRadius �ݰ����� ������ ���Ǿ� ����
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius * radiusValue);

        // ������ ���Ǿ� ���� ��� �ݶ��̴� Ȯ��
        foreach (var hitCollider in hitColliders)
        {
            // AttackValue ��ũ��Ʈ�� ���� ��ü���� Ȯ��
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
