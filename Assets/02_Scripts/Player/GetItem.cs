using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject equipment;
    [SerializeField] GameObject state;
    [SerializeField] GameObject skillWindow;
    PlayerAttack playerAttack;
    [SerializeField] List<GameObject> itemsList = new List<GameObject>();
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
                playerAttack.IsInventory(false);

            }
            else
            {
                inventory.SetActive(true);
                playerAttack.IsInventory(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (equipment.activeSelf)
            {
                equipment.SetActive(false);
                playerAttack.IsEquipment(false);

            }
            else
            {
                equipment.SetActive(true);
                playerAttack.IsEquipment(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            if (state.activeSelf)
            {
                state.SetActive(false);
            }
            else
            {
                state.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(skillWindow.activeSelf)
                skillWindow.SetActive(false);
            else
                skillWindow.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(itemsList.Count > 0)
            {
                GameManager.GM.GetItem(itemsList[0].GetComponent<ItemInfo>().type);
                itemsList[0].SetActive(false);
                itemsList.RemoveAt(0);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item")
        {
            itemsList.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item" && itemsList.Count > 0)
        {
            itemsList.Clear();
        }
    }
}
