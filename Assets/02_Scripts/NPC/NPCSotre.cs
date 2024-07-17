using UnityEngine;

public class NPCSotre : NPCDialogue
{
    private GameObject useItem;
    private GameObject store;
    private GameObject playerStat;
    protected override void Start()
    {
        base.Start();
        Transform canvas = GameObject.Find("Canvas_Player").transform;
        useItem = canvas.transform.GetChild(7).gameObject;
        store = canvas.GetChild(1).gameObject;
        playerStat = canvas.GetChild(4).gameObject;
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
        useItem.SetActive(false);
    }
}
