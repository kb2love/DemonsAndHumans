using UnityEngine;

public class GetItem : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject equipment;
    [SerializeField] GameObject state;
    [SerializeField] GameObject skillWindow;
    PlayerAttack playerAttack;
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Item")
        {

        }
    }
}
