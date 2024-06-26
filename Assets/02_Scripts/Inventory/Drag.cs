using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] RectTransform inventoryTr;
    [SerializeField] RectTransform equipmentTr;
    private RectTransform itemTr;
    private RectTransform saveTr;
    public Vector2 origPos;
    private CanvasGroup canvasGroup;
    ItemInfo itemInfo;
    public static GameObject DraggingItem = null;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemTr = GetComponent<RectTransform>();
        itemInfo = GetComponent<ItemInfo>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        DraggingItem = this.transform.gameObject;
        canvasGroup.blocksRaycasts = false;
        origPos = itemTr.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemTr.position = Input.mousePosition;
        Vector2 pos = itemTr.position;
        if ((100.0f < pos.x && pos.x < 750.0f) && (200.0f < pos.y && pos.y < 900.0f))
        {
            itemTr.SetParent(equipmentTr);
            saveTr = equipmentTr;
        }
        else if ((1000.0f < pos.x && pos.x < 1800.0f) && (150.0f < pos.y && pos.y < 900.0f))
        {
            itemTr.SetParent(inventoryTr);
            saveTr = inventoryTr;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggingItem = null;
        canvasGroup.blocksRaycasts = true;
        if (itemTr.parent == saveTr)
        {
            itemTr.position = origPos;
        }
        if(transform.parent.GetComponent<ItemInfo >() != null)
        {
            Debug.Log(transform.parent);
            Debug.Log(transform.parent.GetComponent<ItemInfo>().type);
            Debug.Log(itemInfo.type);
            if (transform.parent.gameObject.GetComponent<ItemInfo>().type == itemInfo.type)
            {
                ItemTypeSelect(true);
            }
            else if(transform.parent.GetComponent<ItemInfo>().type != itemInfo.type)
            {
                Debug.Log(123);
                ItemTypeSelect(false);
                itemTr.position = origPos;
            }
        }
        else
        {
            ItemTypeSelect(false);
        }
    }

    private void ItemTypeSelect(bool _bool)
    {
        switch (itemInfo.type)
        {
            case ItemData.ItemType.Sword:
                GameManager.GM.SwordOnOff(_bool);
                break;
            case ItemData.ItemType.Shield:
                GameManager.GM.ShieldTakeOff(_bool);
                break;
        }
    }
}
