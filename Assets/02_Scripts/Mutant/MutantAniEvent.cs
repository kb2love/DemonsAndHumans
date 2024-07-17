using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantAniEvent : MonoBehaviour
{
    [SerializeField] float damage;
    public float attackRadius = 3.0f; // 공격 범위

    public void MutantAttack()
    {
        // 스켈레톤의 위치에서 attackRadius 반경으로 오버랩 스피어 생성
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);

        // 오버랩 스피어 내의 모든 콜라이더 확인
        foreach (var hitCollider in hitColliders)
        {
            // AttackValue 스크립트를 가진 객체인지 확인
            PlayerDamage playerDamage = hitCollider.GetComponent<PlayerDamage>();
            if (playerDamage != null)
            {
                playerDamage.HitDamage(damage);
            }
        }
    }
}
