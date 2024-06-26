using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    private void Start()
    {
        SkillManager.skillInst.Level5();
        StatInitialization();
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
            SkillManager.skillInst.Level5();
    }
    void StatInitialization()
    {
        playerData.Level = 1;
        playerData.maxExpValue = 100;
        playerData.MaxHP = 100;
        playerData.MaxMP = 100;
        playerData.HP = 100;
        playerData.MP = 100;
        playerData.AttackValue = 10;
        playerData.DefenceValue = 5;
        playerData.FatalValue = 150;
        playerData.FatalProbability = 0.05f;
    }
}
