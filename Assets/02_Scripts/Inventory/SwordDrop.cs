using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwordDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject sword;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0 && Drag.DraggingItem.GetComponent<ItemInfo>().type == ItemData.ItemType.Sword)
        {
            Drag.DraggingItem.transform.SetParent(transform, false);
            NPCPaladinDialouge npc = FindObjectOfType<NPCPaladinDialouge>();
            if(npc != null)
                npc.IsWeapon(true);
            sword.SetActive(true);  
        }
    }
}
