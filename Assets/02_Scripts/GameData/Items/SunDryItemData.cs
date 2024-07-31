using UnityEngine;
[CreateAssetMenu(fileName = "SunDryItemData", menuName = "ScriptableObjects/SunDryItemData", order = 2)]
public class SunDryItemData : ScriptableObject
{
    public Sprite goldImage;
    public GameObject gold;
    public GameObject normalItem;
    public GameObject equipmentItem;
    public GameObject itemCountText;
    public int goldIdx;
}
