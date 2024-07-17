using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] SwordData swordData;
    [SerializeField] ShieldData shieldData;
    [SerializeField] QuestData03 questData;
    private void Start()
    {
        SkillManager.skillInst.Level5();
        
    }
    private void Awake()
    {
        StatInitialization();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Debug.Log(Input.mousePosition);
    }
    void StatInitialization()
    {
        /*playerData.Level = 1;
        playerData.maxExpValue = 100;
        playerData.MaxHP = 100;
        playerData.MaxMP = 100;
        playerData.HP = 100;
        playerData.MP = 100;
        playerData.AttackValue = 10;
        playerData.DefenceValue = 5;
        playerData.FatalValue = 150;
        playerData.FatalProbability = 0.05f;
        playerData.GoldValue = 10000000;
        swordData.sword01Count = 0;
        shieldData.shield01Count = 0;*/
        questData.Result = false;
    }
}
