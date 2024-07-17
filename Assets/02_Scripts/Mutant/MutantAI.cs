using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class MutantAI : MonoBehaviour
{
    [SerializeField] Transform hpTr;
    [SerializeField] Transform respawnTr;
    NavMeshAgent agent;
    Animator animator;
    private bool isDie = false;
    private bool isOut = false;
    private bool isSpawn = false;
    private Transform playerTr;
    Vector3 originPos;
    Quaternion rot;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerTr = GameObject.FindWithTag("Player").transform;  
    }
    private void OnEnable()
    {
        originPos = transform.position;
        if(isSpawn)
        {
            StartCoroutine(MutantMove());
            isSpawn = false;
        }
    }
    public IEnumerator MutantMove()
    {
        StartCoroutine(HPController());

        while (!isOut)
        {
            yield return new WaitForSeconds(0.1f);
            float dist = Vector3.Distance(transform.position, playerTr.position);
            agent.autoBraking = false;

            if (isOut)
            {
                agent.isStopped = true;
                StartCoroutine(ComebackOriginPos());
                break;
            }

            if (isDie)
            {
                agent.isStopped = true;
                animator.SetTrigger("DieTrigger");
                Invoke("DieOn", 5.0f);
                break;
            }
            else
            {
                if (dist < 3)
                {
                    Vector3 dis = playerTr.position - transform.position;
                    Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dis), Time.deltaTime * 10.0f);
                    rot.x = rot.z = 0;
                    transform.rotation = rot;
                    agent.isStopped = true;
                    animator.SetTrigger("AttackTrigger");
                }
                else if (dist > 3)
                {
                    agent.isStopped = false;
                    animator.SetFloat("moveSpeed", agent.speed);
                    agent.destination = playerTr.position;
                }
            }
        }
    }

    IEnumerator ComebackOriginPos()
    {
        yield return new WaitForSeconds(3.0f);
        while(isOut)
        {
            yield return null;
            agent.isStopped = false;
            agent.destination = originPos;
            animator.SetFloat("moveSpeed", agent.speed);
            agent.autoBraking = true;
            if(Vector3.Distance(transform.position, originPos) < 1.0f)
            {
                animator.SetFloat("moveSpeed", 0);
                break;
            }
        }
    }
    IEnumerator HPController()
    {
        while(!isOut)
        {
            yield return new WaitForSeconds(0.1f);
            rot = Quaternion.LookRotation(playerTr.position - hpTr.position);
            rot.x = rot.z = 0;
            hpTr.rotation = Quaternion.Slerp(hpTr.rotation, rot, Time.deltaTime * 10.0f);
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
        gameObject.SetActive(false);
        isSpawn = true;
    }
    private void OnDisable()
    {
        isDie = false;
        GetComponent<MutantDamage>().enabled = true;
        transform.position = respawnTr.position;
        transform.rotation = respawnTr.rotation;
        Invoke("OnMutant", Random.Range(2.0f, 6.0f));
    }
    void OnMutant()
    {
        gameObject.SetActive(true) ;
    }

}
