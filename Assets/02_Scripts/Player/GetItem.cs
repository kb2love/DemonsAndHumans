using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject equipment;
    [SerializeField] GameObject state;
    [SerializeField] GameObject skillWindow;
    [SerializeField] GameObject option;
    [SerializeField] GameObject quest;
    PlayerAttack playerAttack;
    [SerializeField] List<GameObject> itemsList = new List<GameObject>();
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
    }
    void Update()
    {
        HandleToggleWindow(KeyCode.Tab, KeyCode.I, inventory, playerAttack.IsInventory);
        HandleToggleWindow(KeyCode.U, equipment, playerAttack.IsEquipment);
        HandleToggleWindow(KeyCode.P, state);
        HandleToggleWindow(KeyCode.K, skillWindow);
        HandleCollectItem(KeyCode.F);
        HandleToggleWindow(KeyCode.J, quest);
        HandleToggleWindow(KeyCode.Escape, option);
    }

    void HandleToggleWindow(KeyCode key, GameObject window, System.Action<bool> onToggleAction = null)
    {
        if (Input.GetKeyDown(key))
        {
            bool isActive = !window.activeSelf;
            window.SetActive(isActive);
            onToggleAction?.Invoke(isActive);
        }
    }

    void HandleToggleWindow(KeyCode key1, KeyCode key2, GameObject window, System.Action<bool> onToggleAction = null)
    {
        if (Input.GetKeyDown(key1) || Input.GetKeyDown(key2))
        {
            bool isActive = !window.activeSelf;
            window.SetActive(isActive);
            onToggleAction?.Invoke(isActive);
        }
    }

    void HandleCollectItem(KeyCode key)
    {
        if (Input.GetKeyDown(key) && itemsList.Count > 0)
        {
            ItemManger.itemInst.GetItem(itemsList[0].GetComponent<ItemInfo>().type);
            itemsList[0].SetActive(false);
            itemsList.RemoveAt(0);
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
