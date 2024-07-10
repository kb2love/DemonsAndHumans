using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwordDrag : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] PlayerData playerData;
    RectTransform inventoryTr;
    RectTransform equipmentTr;
    private RectTransform itemTr;
    private RectTransform saveTr;
    public Vector2 origPos;
    private CanvasGroup canvasGroup;
    ItemInfo itemInfo;
    public static GameObject DraggingItem = null;
    void Start()
    {
        inventoryTr = GameObject.Find("Canvas_Player").transform.GetChild(1).GetComponent<RectTransform>();
        equipmentTr = inventoryTr.transform.parent.GetChild(0).GetComponent<RectTransform>();
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

}
