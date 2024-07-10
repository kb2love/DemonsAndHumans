using UnityEngine;

public class NPCSotre : NPCDialogue
{
    private GameObject store;

    protected override void Start()
    {
        base.Start();
        store = GameObject.Find("Canvas_Player").transform.GetChild(1).gameObject;
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
    }
}
