using UnityEngine;
using DG.Tweening;
public class MutantBoss : MonoBehaviour
{
    [Header("������ �ĸ��� �ڸ���")]
    [SerializeField] GameObject magicShield;
    [SerializeField] GameObject jumpAttackEff;
    [SerializeField] GameObject areaAttackEff;
    [Header("������ �ƽ��𵥿콺")]
    [SerializeField] GameObject iceField;
    [SerializeField] GameObject mateor;
    [SerializeField] Transform mateorTr;
    [SerializeField] GameObject electroBall;
    [SerializeField] GameObject strikeFire;
    [SerializeField] float attackRadius = 5.0f;
    [SerializeField] float damage = 50.0f;
    Transform playerTr;
    MutantDamage mutantDamage;
    private void Start()
    {
        mutantDamage = GetComponent<MutantDamage>();
    }
    public void Initialize()
    {
        playerTr = GameObject.Find("Player").transform;
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
    }
    public void AreaAttack()
    {
        areaAttackEff.SetActive(true);
        Attack(2.0f, 1.5f);
    }
    // ����ü ������
    public void ElectroBall()
    {
        electroBall.transform.position = playerTr.position + Vector3.up * 5.0f;
        electroBall.transform.rotation = Quaternion.LookRotation(playerTr.position - transform.position);
        electroBall.SetActive(true);
    }
    // ����ü ������
    public void FireStrike()
    {
        strikeFire.transform.position = transform.forward;
        strikeFire.transform.rotation = Quaternion.LookRotation(playerTr.position - transform.position);
        strikeFire.SetActive(true);
    }
    // ���׿� ������(�ñر�)
    public void Meteor()
    {
        mateor.transform.position = playerTr.position + Vector3.up * 5.0f;
        mateor.SetActive(true);
    }
    // ������ ���ݳ�����
    public void IceField()
    {
        iceField.SetActive(true);
        Attack(3.0f, 2.0f);
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
    void ShieldOff() {  mutantDamage.IsMutantShield(false); }
}
