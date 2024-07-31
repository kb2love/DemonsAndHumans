using UnityEngine;

public class NPCSotre : NPCDialogue
{
    private GameObject store;
    private GameObject playerStat;
    public override void Initialize()
    {
        base.Initialize();
        store = GameObject.Find("Canvas_NPC").transform.GetChild(1).gameObject;
        GameObject.Find("Canvas_NPC").transform.GetChild(1).GetComponent<StoreOpen>().Initialize();
        playerStat = GameObject.Find("PlayerState");
    }
    protected override void StartDialogue()
    {
        base.StartDialogue();
    }
    public override void TextBox()
    {
        float randomValue = Random.value;
        if (randomValue < 0.5f)
        {
            DialougeOn(eventList[0]);
        }
        else
        {
            DialougeOn(eventList[1]);
        }
        ShowDialogueEntry();
    }

    protected override void EndDialogue()
    {
        base.EndDialogue();
        store.SetActive(true);
        playerStat.SetActive(false);
        playerAttack.enabled = false;
        playerController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
