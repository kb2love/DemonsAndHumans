using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SkeletonAI : MonoBehaviour
{
    [SerializeField] Transform hpTr;
    NavMeshAgent agent;
    Animator animator;
    private bool isDie = false;
    private bool isOut = false;
    private Transform playerTr;
    private bool isHit = false;
    private bool isAttack = false;
    Vector3 originPos;
    Quaternion rot;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerTr = GameObject.FindWithTag("Player").transform;  
        originPos = transform.position;
    }

    public IEnumerator SkeletonMove()
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
                GetComponent<SkeletonDamage>().enabled = false;
                animator.SetTrigger("DieTrigger");
                Invoke("DieOn", 5.0f);
                break;
            }
            else
            {
                if (dist < 3 && !isHit)
                {
                    Vector3 dis = playerTr.position - transform.position;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dis), Time.deltaTime * 10.0f);
                    agent.isStopped = true;
                    animator.SetBool("IsAttack", true);
                    isAttack = true;
                }
                else if (isHit)
                {
                    agent.isStopped = true;
                    animator.SetBool("IsAttack", false);
                    Invoke("IsHitFalse", 1.0f);
                }
                else if (!isHit && !isAttack && dist > 3)
                {
                    agent.isStopped = false;
                    animator.SetBool("IsAttack", false);
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
    public void IsHit(bool _isHit)
    {
        isHit = _isHit;
    }
    public void Isout(bool _bool)
    {
        isOut = _bool;
    }
    private void IsHitFalse()
    {
        isHit = false;
    }
    private void DieOn()
    {
        gameObject.SetActive(false);
    }
    public void IsAttackStop()
    {
        isAttack = false;
    }

}
