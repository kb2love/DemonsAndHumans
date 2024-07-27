using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 2)]
public class ItemData : ScriptableObject
{
    public Sprite goldImage;
    public GameObject gold;
    public GameObject normalItem;
    public GameObject equipmentItem;
    public GameObject itemCountText;
    public int goldIdx;
}
