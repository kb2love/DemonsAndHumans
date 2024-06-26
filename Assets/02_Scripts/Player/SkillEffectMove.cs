using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SkillEffectMove : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] float aniTime = 1;
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
            Debug.Log("123");
            other.GetComponent<SkeletonDamage>().SkeletonHit(playerData.level05MagicDamage);
            gameObject.SetActive(false);
        }
        
    }
    private void OnDisable()
    {
        seq.Kill();
    }
}
