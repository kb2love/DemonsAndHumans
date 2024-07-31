using UnityEngine;
using DG.Tweening;
public class MutantBoss : MonoBehaviour
{
    [Header("군단장 파멸의 자르간")]
    [SerializeField] GameObject magicShield;
    [SerializeField] GameObject jumpAttackEff;
    [SerializeField] GameObject areaAttackEff;
    [Header("군단장 아스모데우스")]
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
    // 투사체 날리기
    public void ElectroBall()
    {
        electroBall.transform.position = playerTr.position + Vector3.up * 5.0f;
        electroBall.transform.rotation = Quaternion.LookRotation(playerTr.position - transform.position);
        electroBall.SetActive(true);
    }
    // 투사체 날리기
    public void FireStrike()
    {
        strikeFire.transform.position = transform.forward;
        strikeFire.transform.rotation = Quaternion.LookRotation(playerTr.position - transform.position);
        strikeFire.SetActive(true);
    }
    // 메테오 날리기(궁극기)
    public void Meteor()
    {
        mateor.transform.position = playerTr.position + Vector3.up * 5.0f;
        mateor.SetActive(true);
    }
    // 전방향 공격날리기
    public void IceField()
    {
        iceField.SetActive(true);
        Attack(3.0f, 2.0f);
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
    void ShieldOff() {  mutantDamage.IsMutantShield(false); }
}
