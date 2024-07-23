using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoreOpen : MonoBehaviour
{
    [SerializeField] RectTransform weqponContant;
    [SerializeField] RectTransform potionContant;
    [SerializeField] RectTransform materialContant;
    [SerializeField] GameObject playerStat;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] Text storeText;
    public void WeaponStore()
    {
        StoreSelect(true, false, false, weqponContant, "무기 상점");
    }
    public void PotionStore()
    {
        StoreSelect(false, true, false, potionContant, "포션 상점");
    }
    public void MaterialStore()
    {
        StoreSelect(false, false, true, materialContant, "판매");
    }
    public void QuitStore()
    {
        gameObject.SetActive(false);
        playerStat.SetActive(true);
        PlayerAttack playerAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        playerAttack.enabled = true;
        playerAttack.GetComponent<PlayerController>().enabled = true;   
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    public void StoreSelect(bool _isWeapon, bool _isPotion, bool _isMaterial, RectTransform rect, string storeName)
    {
        weqponContant.gameObject.SetActive(_isWeapon);
        potionContant.gameObject.SetActive(_isPotion);
        materialContant.gameObject.SetActive(_isMaterial);
        scrollRect.content = rect;
        storeText.text = storeName;
    }

}
