using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 2)]
public class ItemData : ScriptableObject
{
    public enum ItemType { Hat, Cloak, Sword, Shield, Cloth, Ring, Pants, Necklace, Shoes, Material, Gold, HPPotion ,MPPotion, Nothing}
    public ItemType Type;
    public Sprite swordImage;
    public Sprite shieldImage;
    public Sprite goldImage;
    public Sprite hpImage;
    public Sprite mpImage;
    public float swordDamage;
    public int swordIdx;
    public int shieldIdx;
    public int goldIdx;
    public int hpIdx;
    public int mpIdx;
    
}
