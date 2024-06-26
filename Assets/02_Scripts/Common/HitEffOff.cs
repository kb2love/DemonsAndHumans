using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitEffOff : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("ObjectOff", 0.5f);
    }
    void ObjectOff()
    {
        gameObject.SetActive(false);
    }
}
