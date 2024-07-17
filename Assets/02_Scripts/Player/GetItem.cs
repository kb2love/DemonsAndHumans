using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    UnityAction[] actions = new UnityAction[4];

    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        CursorOff();
    }

    void Update()
    {
        HandleToggleWindow(new KeyCode[] { KeyCode.Tab, KeyCode.I }, inventory, playerAttack.IsInventory);
        HandleToggleWindow(new KeyCode[] { KeyCode.U }, equipment, playerAttack.IsEquipment);
        HandleToggleWindow(new KeyCode[] { KeyCode.P }, state);
        HandleToggleWindow(new KeyCode[] { KeyCode.K }, skillWindow);
        HandleCollectItem(KeyCode.F);
        HandleToggleWindow(new KeyCode[] { KeyCode.J }, quest);
        HandleToggleWindow(new KeyCode[] { KeyCode.Escape }, option);
        HandlePotionUse(0);
        HandlePotionUse(1);
        HandlePotionUse(2);
        HandlePotionUse(3);
    }

    void HandleToggleWindow(KeyCode[] keys, GameObject window, System.Action<bool> onToggleAction = null)
    {
        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                bool isActive = !window.activeSelf;
                window.SetActive(isActive);
                onToggleAction?.Invoke(isActive);

                if (isActive)
                {
                    CursorOn();
                }
                else
                {
                    CursorOff();
                }

                break;
            }
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

    void HandlePotionUse(int i)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1 + i) && actions[i] != null)
        {
            actions[i]();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            itemsList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item") && itemsList.Count > 0)
        {
            itemsList.Clear();
        }
    }

    public void SetAction(int index, UnityAction action)
    {
        actions[index] = action;
    }
    public void CursorOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void CursorOn()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
