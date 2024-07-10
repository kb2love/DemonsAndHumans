using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreOpen : MonoBehaviour
{
    [SerializeField] RectTransform weqponContant;
    [SerializeField] RectTransform potionContant;
    [SerializeField] RectTransform materialContant;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] Text storeText;
    public void WeaponStore()
    {
        StoreSelect(true, false, false, weqponContant, "���� ����");
    }
    public void PotionStore()
    {
        StoreSelect(false, true, false, potionContant, "���� ����");
    }
    public void MaterialStore()
    {
        StoreSelect(false, false, true, materialContant, "�Ǹ�");
    }
    public void QuitStore()
    {
        gameObject.SetActive(false);
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
