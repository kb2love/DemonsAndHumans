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
        // ���̷����� ��ġ���� attackRadius �ݰ����� ������ ���Ǿ� ����
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5.0f * radiusValue);

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
}
