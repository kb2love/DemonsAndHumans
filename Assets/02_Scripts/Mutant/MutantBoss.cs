using UnityEngine;
using DG.Tweening;
using System.IO.Compression;
public class MutantBoss : MonoBehaviour
{
    [Header("������ �ĸ��� �ڸ���")]
    [SerializeField] GameObject magicShield;
    [SerializeField] GameObject jumpAttackEff;
    [SerializeField] GameObject areaAttackEff;
    [SerializeField] AudioClip zarganClip;
    [Header("������ �ƽ��𵥿콺")]
    [SerializeField] GameObject iceField;
    [SerializeField] GameObject mateor;
    [SerializeField] GameObject mateor02;
    [SerializeField] Transform mateorTr;
    [SerializeField] GameObject electroBall;
    [SerializeField] GameObject electroBall02;
    [SerializeField] GameObject strikeFire;
    [SerializeField] GameObject strikeFire02;

    [SerializeField] AudioClip[] aduios;
    [SerializeField] float attackRadius = 5.0f;
    [SerializeField] float damage = 50.0f;
    AudioSource audioSource;
    Transform playerTr;
    MutantDamage mutantDamage;
    private void Start()
    {
        mutantDamage = GetComponent<MutantDamage>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Initialize()
    {
        playerTr = GameObject.Find("Player").transform;
    }
    // �ڸ���
    public void MagicShield()
    {
        magicShield.SetActive(true);
        mutantDamage.IsMutantShield(true);
        SoundManager.soundInst.EffectSoundPlay(audioSource, zarganClip);
        Invoke("ShieldOff", 2.0f);
    }
    void ShieldOff() { mutantDamage.IsMutantShield(false); }
    public void JumpAttack()
    {
        jumpAttackEff.SetActive(true);
        SoundManager.soundInst.EffectSoundPlay(audioSource, zarganClip);
        Attack(1.5f, 2.0f);
    }
    public void AreaAttack()
    {
        areaAttackEff.SetActive(true);
        SoundManager.soundInst.EffectSoundPlay(audioSource, zarganClip);
        Attack(2.0f, 1.5f);
    }
    // �ƽ��𵥿콺
    // ����ü ������
    public void ElectroBall()
    {
        if (!electroBall.activeSelf)
        {
            electroBall.transform.position = playerTr.position + Vector3.up * 5.0f;
            electroBall.transform.rotation = Quaternion.LookRotation(playerTr.position - transform.position);
            electroBall.SetActive(true);
        }
        else
        {
            electroBall02.transform.position = playerTr.position + Vector3.up * 5.0f;
            electroBall02.transform.rotation = Quaternion.LookRotation(playerTr.position - transform.position);
            electroBall02.SetActive(true);
        }
        SoundManager.soundInst.EffectSoundPlay(audioSource, aduios[0]);
    }
    // ����ü ������
    public void FireStrike()
    {
        if (!strikeFire.activeSelf)
        {
            strikeFire.transform.position = transform.position + transform.forward;
            strikeFire.transform.rotation = Quaternion.LookRotation(playerTr.position - transform.position);
            strikeFire.SetActive(true);
        }
        else
        {
            strikeFire02.transform.position = transform.position + transform.forward;
            strikeFire02.transform.rotation = Quaternion.LookRotation(playerTr.position - transform.position);
            strikeFire02.SetActive(true);
        }
        Attack(3.0f, 2.0f);
        SoundManager.soundInst.EffectSoundPlay(audioSource, aduios[1]);
    }
    // ���׿� ������(�ñر�)
    public void Meteor()
    {
        if (!mateor.activeSelf)
        {
            mateor.transform.position = playerTr.position + Vector3.up * 5.0f;
            mateor.SetActive(true);
        }
        else
        {
            mateor02.transform.position = playerTr.position + Vector3.up * 5.0f;
            mateor02.SetActive(true);
        }
        SoundManager.soundInst.EffectSoundPlay(audioSource, aduios[2]);
    }
    // ������ ���ݳ�����
    public void IceField()
    {
        iceField.SetActive(true);
        SoundManager.soundInst.EffectSoundPlay(audioSource, aduios[3]);
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
}
