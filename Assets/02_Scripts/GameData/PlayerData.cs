using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public AudioClip levelUpClip;
    public AudioClip ItemGetClip;
    public GameObject hitEff;
    public GameObject iceHitEff;
    public GameObject fireHitEff;
    public GameObject electroHitEff;
    public GameObject iceSpear;
}
