using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantFieldSceneMove : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneMove.SceneInst.MutantFieldScene();
        }
    }
}
