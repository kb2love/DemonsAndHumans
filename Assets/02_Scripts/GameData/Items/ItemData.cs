using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 2)]
public class ItemData : ScriptableObject
{
    public enum ItemType { Sword, Shield, Hat, Cloth, Pants, Shoes, Neck, Kloak, Ring, Material, Gold, HPPotion ,MPPotion,Nothing}
    public ItemType Type;
    public Sprite goldImage;
    public Sprite hpImage;
    public Sprite mpImage;
    public GameObject gold;
    public GameObject normalItem;
    public GameObject equipmentItem;
    public GameObject itemCountText;
    public int goldIdx;
    public int hpIdx;
    public int mpIdx;
    
}
