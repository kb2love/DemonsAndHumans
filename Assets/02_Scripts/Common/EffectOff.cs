using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectOff : MonoBehaviour
{
    [SerializeField] float EffOffTime;
    void OnEnable()
    {
        Invoke("ObjectOff", EffOffTime);
    }
    void ObjectOff()
    {
        gameObject.SetActive(false);
    }
}
