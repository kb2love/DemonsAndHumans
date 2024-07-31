using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MutantAI : MonoBehaviour
{
    [SerializeField] Transform hpTr;
    [SerializeField] Transform respawnTr;
    [Header("���� ȿ����")]
    [SerializeField] AudioClip attackClip;
    [SerializeField] AudioClip dieClip;
    AudioSource audioSource;
    [SerializeField] bool mutantKindIdx;
    [SerializeField] int mutantAttackKindIdx;
    [SerializeField] float damage;
    [SerializeField] float attackRadius = 3.0f; // ���� ����
    [SerializeField] GameObject bossImage;

    public enum MutantState { Idle, Trace, Attack, Return, Die }
    public MutantState state;

    NavMeshAgent agent;
    Animator animator;
    private bool isDie = false;
    private bool isOut = false;
    private bool isSpawn = false;
    private bool isStop = false;
    private Transform playerTr;
    Vector3 originPos;
    Quaternion rot;

    public void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerTr = GameObject.FindWithTag("Player").transform;
        originPos = transform.position;
    }

    private void OnEnable()
    {
        if(isOut)
            state = MutantState.Idle;
        else if (isSpawn && !isOut)
        {
            MutantBehavior();
            isSpawn = false;
        }
    }

    public void MutantBehavior()
    {
        isOut = false;
        isDie = false;
        StartCoroutine(MutantDist());
        StartCoroutine(MutantMove());
    }

    public IEnumerator MutantDist()
    {
        while (!isOut && !isDie)
        {
            yield return null;
            float dist = Vector3.Distance(transform.position, playerTr.position);
            if (isOut)
            {
                state = MutantState.Return;
            }
            else if (isDie)
            {
                state = MutantState.Die;
            }
            else if (dist < attackRadius)
            {
                state = MutantState.Attack;
            }
            else if (dist > attackRadius && !isStop)
            {
                state = MutantState.Trace;
            }
        }
    }

    public IEnumerator MutantMove()
    {
        StartCoroutine(HPController());

        while (!isDie)
        {
            yield return null;
            agent.autoBraking = false;

            switch (state)
            {
                case MutantState.Idle:
                    animator.SetFloat("moveSpeed", 0f);
                    agent.isStopped = true;
                    isDie = true;
                    break;
                case MutantState.Trace:
                    agent.isStopped = false;
                    animator.SetFloat("moveSpeed", agent.speed);
                    agent.destination = playerTr.position;
                    break;
                case MutantState.Attack:
                    isStop = true;
                    Vector3 direction = playerTr.position - transform.position;
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);
                    agent.isStopped = true;
                    if (mutantKindIdx)
                        animator.SetInteger("AttackIdx", Random.Range(0, mutantAttackKindIdx));
                    if(!audioSource.isPlaying)
                        SoundManager.soundInst.EffectSoundPlay(audioSource, attackClip);
                    animator.SetTrigger("AttackTrigger");
                    Invoke("IsTraceFalse", 3.0f);
                    break;
                case MutantState.Return:
                    agent.isStopped = false;
                    agent.destination = originPos;
                    animator.SetFloat("moveSpeed", agent.speed);
                    if (Vector3.Distance(transform.position, originPos) < 1.0f)
                    {
                        StartCoroutine(SlowDownMoveSpeed());
                        state = MutantState.Idle;
                    }
                    break;
                case MutantState.Die:
                    agent.isStopped = true;
                    animator.SetTrigger("DieTrigger");
                    SoundManager.soundInst.EffectSoundPlay(dieClip);
                    Invoke("DieOn", 5.0f);
                    if (bossImage != null)
                    {
                        bossImage.GetComponent<CanvasGroup>().DOFade(0, 2.0f);
                    }
                    break;
            }
        }
    }

    IEnumerator SlowDownMoveSpeed()
    {
        float currentSpeed = animator.GetFloat("moveSpeed");
        float targetSpeed = 0f;
        float duration = 1f; // õõ�� ���̴� �ð�
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, elapsedTime / duration);
            animator.SetFloat("moveSpeed", newSpeed);
            yield return null;
        }

        animator.SetFloat("moveSpeed", targetSpeed);
        agent.isStopped = true;
    }

    IEnumerator HPController()
    {
        while (!isOut)
        {
            yield return new WaitForSeconds(0.1f);
            if (hpTr != null)
            {
                rot = Quaternion.LookRotation(playerTr.position - hpTr.position);
                rot.x = rot.z = 0;
                hpTr.rotation = Quaternion.Slerp(hpTr.rotation, rot, Time.deltaTime * 10.0f);
            }
        }
    }

    public void IsDie(bool _isDie)
    {
        isDie = _isDie;
    }

    public void Isout(bool _bool)
    {
        isOut = _bool;
    }

    private void DieOn()
    {
        isSpawn = true;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        isDie = false;
        GetComponent<MutantDamage>().enabled = true;
        if(respawnTr != null)
        {
            transform.position = respawnTr.position;
            transform.rotation = respawnTr.rotation;
            Invoke("OnMutant", Random.Range(2.0f, 6.0f));
        }
    }

    void OnMutant()
    {
        gameObject.SetActive(true);
    }

    void IsTraceFalse() { isStop = false; }

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
