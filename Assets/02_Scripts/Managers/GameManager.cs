using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] ItemData itemData;
    [SerializeField] Transform inventory;
    [SerializeField] PlayerData playerData;
    [SerializeField] List<Text> state = new List<Text>();
    [SerializeField] List<GameObject> items;
    [SerializeField] Image expImage;
    Transform itemGroup;
    List<RectTransform> itemList = new List<RectTransform>();
    List<RectTransform> windowList = new List<RectTransform>();
    PlayerAttack playerAttack;
    public static GameManager GM;
    private void Awake()
    {
        if (GM == null)
            GM = this;
        else if (GM != this)
            Destroy(GM);
        DontDestroyOnLoad(GM);
    }
    private void Start()
    {
        Transform window = inventory.GetChild(0).transform;
        for (int i = 0; i < window.childCount; i++)
        {
            windowList.Add(window.GetChild(i).GetComponent<RectTransform>());
        }
        itemGroup = inventory.GetChild(1).transform;
        for (int i = 0; i < itemGroup.childCount; i++)
        {
            itemList.Add(itemGroup.GetChild(i).GetComponent<RectTransform>());
        }
        GetItem(ItemData.ItemType.Sword);
        GetItem(ItemData.ItemType.Shield);
        playerAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        AllStatUpdata();
    }
    public void GetItem(ItemData.ItemType itemType)
    {
        switch (itemType)
        {
            case ItemData.ItemType.Sword:
                GetSword();
                break;
            case ItemData.ItemType.Shield:
                GetShield();
                break;
            case ItemData.ItemType.Gold:
                GetItemCase(itemData.swordImage, ItemData.ItemType.Gold, itemData.swordIdx);
                break;
        }
    }
    public void StatUpdate(PlayerData.PlayerStat playerStat)
    {
        switch (playerStat)
        {
            case PlayerData.PlayerStat.Level:
                state[0].text = "Level : " + playerData.Level.ToString();
                state[9].text = "Lev : " + playerData.Level.ToString(); 
                break;
            case PlayerData.PlayerStat.HP:
                state[1].text = "HP : " + playerData.HP.ToString("0");
                break;
            case PlayerData.PlayerStat.MP:
                state[2].text = "MP : " + playerData.MP.ToString("0");
                break;
            case PlayerData.PlayerStat.MaxHP:
                state[3].text = "MaxHP : " + playerData.MaxHP.ToString("0");
                break;
            case PlayerData.PlayerStat.MaxMP:
                state[4].text = "MaxMP : " + playerData.MaxMP.ToString("0");
                break;
            case PlayerData.PlayerStat.AttackValue:
                state[5].text = "공격력 : " + playerData.AttackValue.ToString("0");
                break;
            case PlayerData.PlayerStat.DefenceValue:
                state[6].text = "방어력 : " + playerData.DefenceValue.ToString("0");
                break;
            case PlayerData.PlayerStat.FatalProbability:
                state[7].text = "치명타 확률 : " + (playerData.FatalProbability * 100).ToString("0.0") + "%";
                break;
            case PlayerData.PlayerStat.FatalValue:
                state[8].text = "치명타 공격력 : " + playerData.FatalValue.ToString("0") + "%";
                break;
        }
    }
    public void ExpUp(int exp)
    {
        playerData.expValue += exp;
        if (playerData.expValue >= playerData.maxExpValue )
        {
            LevelUp();
        }
        expImage.fillAmount = playerData.expValue / playerData.maxExpValue;
    }
    private void LevelUp()
    {
        playerData.expValue -= playerData.maxExpValue;
        playerData.maxExpValue *= 1.1f;
        playerData.MaxHP *= 1.1f;
        playerData.MaxMP *= 1.1f;
        playerData.HP = playerData.MaxHP;
        playerData.MP = playerData.MaxMP;
        playerData.AttackValue *= 1.1f;
        playerData.DefenceValue *= 1.1f;
        playerData.FatalValue *= 1.05f;
        playerData.FatalProbability *= 1.1f;
        playerData.Level++;
        for(int i = 0; i < 10; i++)
        {
            AllStatUpdata();
        }
        StatUpdate(PlayerData.PlayerStat.Level);
    }

    private void AllStatUpdata()
    {
        StatUpdate(PlayerData.PlayerStat.Level);
        StatUpdate(PlayerData.PlayerStat.HP);
        StatUpdate(PlayerData.PlayerStat.MP);
        StatUpdate(PlayerData.PlayerStat.MaxHP);
        StatUpdate(PlayerData.PlayerStat.MaxMP);
        StatUpdate(PlayerData.PlayerStat.AttackValue);
        StatUpdate(PlayerData.PlayerStat.DefenceValue);
        StatUpdate(PlayerData.PlayerStat.FatalValue);
        StatUpdate(PlayerData.PlayerStat.FatalProbability);
    }

    private void GetSword()
    {
        GetItemCase(itemData.swordImage, ItemData.ItemType.Sword, itemData.swordIdx);
    }
    private void GetShield()
    {
        GetItemCase(itemData.shieldImage, ItemData.ItemType.Shield, itemData.shieldIdx);
    }
    void GetItemCase(Sprite itemImage, ItemData.ItemType type, int idx)
    {
        RectTransform item = GetItemList();
        RectTransform window = GetWindowList(idx);
        item.GetComponent<Image>().sprite = itemImage;
        item.GetComponent<ItemInfo>().type = type;
        item.SetParent(window);
        item.gameObject.SetActive(true);
    }
    RectTransform GetItemList()
    {
        foreach (RectTransform item in itemList)
        {
            if (item.parent == itemGroup)
            {
                return item;
            }
        }
        return null;
    }
    RectTransform GetWindowList(int idx)
    {
        foreach (RectTransform window in windowList)
        {
            if (window.childCount == 0)
            {
                return window;
            }
        }
        for (int i = 0; i < windowList.Count; i++)
        {
            if (windowList[i].childCount == 0)
            {
                idx = i;
                Debug.Log(idx);
                return windowList[i];
            }
        }
        return null;
    }
    public void SwordOnOff(bool _active)
    {
        items[0].SetActive(_active);
        playerAttack.IsSword(_active);

    }
    public void ShieldTakeOff(bool _active)
    {
        items[1].SetActive(_active);
        playerAttack.IsShield(_active);
    }
}
