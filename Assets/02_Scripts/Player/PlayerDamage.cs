using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] Transform shieldTr;
    [SerializeField] Image plHp;
    PlayerController playerController;
    Animator animator;

    GameObject hitEff;
    bool isShield;
    private bool isHit = false;
    public bool IsHit
    {
        get
        {
            return isHit;
        }
    }
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
        plHp.fillAmount = playerData.HP / playerData.MaxHP;
    }
    public void HitDamage(float damage)
    {
        hitEff = ObjectPoolingManager.objInst.GetHitEff();
        animator.SetTrigger("HitTrigger");
        if (!isShield)
        {
            hitEff.transform.position = transform.position + (Vector3.up * 0.8f);
            playerData.HP -= damage;
            plHp.fillAmount = playerData.HP / playerData.MaxHP;
            GameManager.GM.StatUpdate(PlayerData.PlayerStat.HP);
            if (playerData.HP <= 0)
            {
                Debug.Log("너죽음 ㅋㅋㅋ");
            }
        }
        else
        {
            Debug.Log("막앗죠?");
            hitEff.transform.position = shieldTr.position;
            isHit = true;
            Invoke("IsHitFalse", 0.5f);
        }
        hitEff.SetActive(true);
    }
    // 아이템 얻는 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            ItemManager.itemInst.GetItem(other.gameObject.GetComponent<ItemInfo>().type);
            SoundManager.soundInst.EffectSoundPlay(playerData.ItemGetClip);
            other.gameObject.SetActive(false);
        }
    }

    public void IsShieldOnOff(bool _isShield)
    {
        isShield = _isShield;
    }
    private void IsHitFalse()
    {
        isHit = false;
    }
}
