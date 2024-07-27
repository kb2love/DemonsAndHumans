using UnityEditor.Build;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] PlayerData playerData;
    [SerializeField] ItemData itemData;
    [SerializeField] RectTransform inventoryTr;
    [SerializeField] RectTransform equipmentTr;
    private RectTransform itemTr;
    public Vector2 origPos;
    private CanvasGroup canvasGroup;
    ItemInfo itemInfo;
    public static GameObject DraggingItem = null;
    //************ 아이템 값을 저장할 변수*******************//
    public float hp = 0;
    public float mp = 0;
    public float damage = 0;
    public float defence = 0;
    public float magicDamage = 0;
    public float fatalProbabillity = 0;
    public float fatalValue = 0;
    public int price = 0;
    bool isEquip;
    Transform parent;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemTr = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        itemInfo = GetComponent<ItemInfo>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {//클릭하면 DraaginItem을 나로 바꾼다
        DraggingItem = this.transform.gameObject;
        canvasGroup.blocksRaycasts = false;
        origPos = itemTr.position;
        parent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {// 클릭하는동안 위치를 판단하여 위치가 인벤토리 쪽이냐 장비쪽이냐에 따라 부모가 달라짐
        itemTr.position = Input.mousePosition;
        Vector2 pos = itemTr.position;
        if ((100.0f < pos.x && pos.x < 750.0f) && (200.0f < pos.y && pos.y < 900.0f))
        {
            itemTr.SetParent(equipmentTr);
        }
        else if ((1000.0f < pos.x && pos.x < 1800.0f) && (150.0f < pos.y && pos.y < 900.0f))
        {
            itemTr.SetParent(inventoryTr);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggingItem = null;
        canvasGroup.blocksRaycasts = true;
        if (itemTr.parent == equipmentTr || itemTr.parent == inventoryTr)
        {//내 부모가 인벤토리에 들어간게아닌 인벤토리창, 장비창에머물러있다면 원래있던 위치로 돌린다
            transform.SetParent(parent);
        }
        if (transform.parent.GetComponent<ItemInfo>() != null)
        {
            if (transform.parent.gameObject.GetComponent<ItemInfo>().type == itemInfo.type)
            {// 아이템이 장비창과 같다면 
                if (itemInfo.type == ItemType.Sword || itemInfo.type == ItemType.Shield)
                    ItemTypeSelect(itemInfo.type, true);
                StatUp();
                isEquip = true;
                GameManager.GM.AllStatUpdata();
                ItemManager.itemInst.AllItemSave();
            }
            else if (transform.parent.GetComponent<ItemInfo>().type != itemInfo.type)
            {
                if (itemInfo.type == ItemType.Sword || itemInfo.type == ItemType.Shield)
                    ItemTypeSelect(itemInfo.type, false);
                transform.SetParent(parent);
                if (isEquip)
                    StatDown();
                isEquip = false;
                GameManager.GM.AllStatUpdata();
                ItemManager.itemInst.AllItemSave();
            }
        }
        else
        {
            if (itemInfo.type == ItemType.Sword || itemInfo.type == ItemType.Shield)
                ItemTypeSelect(itemInfo.type, false);
            if (isEquip)
                StatDown();
            isEquip = false;
            GameManager.GM.AllStatUpdata();
            ItemManager.itemInst.AllItemSave();
        }
    }

    private void StatUp()
    {
        playerData.HP += hp;
        playerData.MP += mp;
        playerData.AttackValue += damage;
        playerData.DefenceValue += defence;
        playerData.MagicAttackValue += magicDamage;
        playerData.FatalProbability += fatalProbabillity;
        playerData.FatalValue += fatalValue;
    }
    private void StatDown()
    {
        playerData.HP -= hp;
        playerData.MP -= mp;
        playerData.AttackValue -= damage;
        playerData.DefenceValue -= defence;
        playerData.MagicAttackValue -= magicDamage;
        playerData.FatalProbability -= fatalProbabillity;
        playerData.FatalValue -= fatalValue;
    }


    public void ItemTypeSelect(ItemType type, bool _bool)
    {
        switch (type)
        {
            case ItemType.Sword:
                ItemManager.itemInst.SwordOnOff(_bool);
                break;
            case ItemType.Shield:
                ItemManager.itemInst.ShieldTakeOff(_bool);
                break;
        }
    }
    public void ItemChange(ItemType type, float value01)
    {
        switch (type)
        {
            case ItemType.Sword:
                damage = value01;
                break;
            case ItemType.Shield:
                defence = value01;
                break;
        }
    }
    public void ItemChange(ItemType type, float HP, float Defence)
    {
        switch (type)
        {
            case ItemType.Hat:
                hp = HP;
                defence = Defence;
                break;
            case ItemType.Cloth:
                hp = HP;
                defence = Defence;
                break;
            case ItemType.Pants:
                hp = HP;
                defence = Defence;
                break;
            case ItemType.Shoes:
                hp = HP;
                defence = Defence;
                break;
        }
    }
    public void ItemChange(ItemType type, float HPAndMP, float MPAndDamage, float DefenceAndMagiceDamage)
    {
        switch (type)
        {
            case ItemType.Kloak:
                hp = HPAndMP;
                mp = MPAndDamage;
                defence = DefenceAndMagiceDamage;
                break;
            case ItemType.Neck:
                mp = MPAndDamage;
                damage = MPAndDamage;
                magicDamage = DefenceAndMagiceDamage;
                break;
        }
    }
    public void ItemChange(ItemType type, float HP, float MP, float Damage, float Defence, float MagicDamage, float FatalProbabillity, float FatalValue)
    {
        switch (type)
        {
            case ItemType.Ring:
                hp = HP;
                mp = MP;
                defence = Defence;
                fatalValue = FatalValue;
                fatalProbabillity = FatalProbabillity;
                magicDamage = MagicDamage;
                damage = Damage;
                break;
        }
    }
    public void IsEuqip(bool _isEquip)
    {
        isEquip = _isEquip;
    }
}
