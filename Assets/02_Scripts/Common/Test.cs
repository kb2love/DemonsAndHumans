using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.H))
        {
            GameManager.GM.ExpUp(100);
            ItemManager.itemInst.GoldPlus(1000);
        }
    }
}
