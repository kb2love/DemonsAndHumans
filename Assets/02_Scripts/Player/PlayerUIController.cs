using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject equipment;
    [SerializeField] GameObject state;
    [SerializeField] GameObject skillWindow;
    [SerializeField] GameObject option;
    [SerializeField] GameObject quest;
    PlayerAttack playerAttack;

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
                    playerAttack.enabled = false;
                }
                else
                {
                    CursorOff();
                    playerAttack.enabled = true;
                }

                break;
            }
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
            ItemManager.itemInst.GetItem(other.gameObject.GetComponent<ItemInfo>().type);
            other.gameObject.SetActive(false);
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
