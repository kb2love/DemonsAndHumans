using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantAniEvent : MonoBehaviour
{
    [SerializeField] float damage;
    public float attackRadius = 3.0f; // ���� ����

    public void MutantAttack()
    {
        // ���̷����� ��ġ���� attackRadius �ݰ����� ������ ���Ǿ� ����
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);

        // ������ ���Ǿ� ���� ��� �ݶ��̴� Ȯ��
        foreach (var hitCollider in hitColliders)
        {
            // AttackValue ��ũ��Ʈ�� ���� ��ü���� Ȯ��
            PlayerDamage playerDamage = hitCollider.GetComponent<PlayerDamage>();
            if (playerDamage != null)
            {
                playerDamage.HitDamage(damage);
            }
        }
    }
}
