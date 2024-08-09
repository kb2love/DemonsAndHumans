using UnityEngine;
using DG.Tweening;
public class MutantSkillMove : MonoBehaviour
{
    [SerializeField] float damage = 100.0f;
    [SerializeField] float speed = 2.0f;
    [SerializeField] GameObject meteorField;
    [SerializeField] bool isMeteor;
    [SerializeField] AudioClip boomClip;
    void OnEnable()
    {
        if(isMeteor)
        {
            Vector3 playerPos = GameObject.Find("Player").transform.position;
            transform.DOMove(playerPos, speed).OnComplete(() =>
            {
                gameObject.SetActive(false);
                meteorField.transform.position = transform.position + (Vector3.up * 1.0f);
                SoundManager.soundInst.EffectSoundPlay(boomClip);
                meteorField.SetActive(true);
                Attack(1.5f, 3.0f);
            });
        }
        else
        {
            Vector3 playerPos = GameObject.Find("Player").transform.position;
            Vector3 dis = playerPos - transform.position;
            transform.rotation = Quaternion.LookRotation(dis);
            transform.DOMove(playerPos, speed).OnComplete(() => gameObject.SetActive(false));
            Attack(1.0f, 2.0f);
        }
        
    }
    void Attack(float radiusValue, float damageValue)
    {
        // 스켈레톤의 위치에서 attackRadius 반경으로 오버랩 스피어 생성
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5.0f * radiusValue);

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
}
