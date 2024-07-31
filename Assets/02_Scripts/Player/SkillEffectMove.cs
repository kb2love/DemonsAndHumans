using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SkillEffectMove : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float aniTime = 1;
    [SerializeField] SkillState skillState;
    Transform pltr;
    Sequence seq;
    private void Start()
    {
        pltr = GameObject.FindWithTag("Player").transform;
    }
    void OnEnable()
    {
        seq = DOTween.Sequence();
        Vector3 targetPos = transform.position + transform.forward * 100;
        seq.Append(transform.DOMove(targetPos, aniTime)).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Skeleton")
        {
            other.GetComponent<MutantDamage>().HitEffChange(skillState);
            other.GetComponent<MutantDamage>().MutantHit(damage);
            gameObject.SetActive(false);
        }
        
    }
    private void OnDisable()
    {
        seq.Kill();
    }
}
