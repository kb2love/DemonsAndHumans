using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 2)]
public class ItemData : ScriptableObject
{
    public enum ItemType { Sword, Shield, Hat, Cloth, Pants, Shoes, Neck, Kloak, Ring, Material, Gold, HPPotion ,MPPotion,Nothing}
    public ItemType Type;
    public Sprite goldImage;
    public GameObject gold;
    public GameObject normalItem;
    public GameObject equipmentItem;
    public GameObject itemCountText;
    public int goldIdx;
    [Header("무기 장착 여부")]
    public bool isSword;
    public bool isShield;
    public bool isHat;
    public bool isCloth;
    public bool isPants;
    public bool isShoes;
    public bool isKloak;
    public bool isNeck;
    public bool isRing;

}
