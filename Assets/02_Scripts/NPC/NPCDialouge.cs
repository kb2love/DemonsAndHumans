using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] protected List<string> eventList = new List<string>();
    [Header("3D My Mark")]
    [SerializeField] protected GameObject my3DExcMark;
    [SerializeField] protected GameObject my3DQuesMark;
    [Header("2D My Mark")]
    [SerializeField] protected GameObject my2DExcMark;
    [SerializeField] protected GameObject my2DQuesNpcMark;
    [SerializeField] protected GameObject my2DNPCMark;
    public QuestState questState01 = QuestState.QuestHave;
    protected GameObject textBGImage;
    protected Text dialogueText;
    protected Text characterName;
    protected Animator animator;
    protected PlayerAttack playerAttack;
    protected PlayerController playerController;
    protected CameraController cameraController;
    protected List<DialogueEntry> dialogueEntries;
    protected int currentDialogueIndex;
    protected int dialogueIdx = 0;
    private Quaternion originRot;

    public virtual void Initialize()
    {
        textBGImage = GameObject.Find("Canvas_NPC").transform.GetChild(0).gameObject;
        dialogueText = textBGImage.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        characterName = textBGImage.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerAttack = playerController.GetComponent<PlayerAttack>();
        cameraController = Camera.main.transform.parent.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        originRot = transform.rotation;
    }

    public virtual void TextBox()
    {
        StartDialogue();
        if (dialogueIdx < eventList.Count && currentDialogueIndex == 0)
        {
            DialougeOn(eventList[dialogueIdx]);
        }
        ShowDialogueEntry();
    }

    protected void DialougeOn(string _eventName)
    {
        dialogueEntries = DialogueManager.dialogueInst.GetDialogueByEvent(_eventName);
        
        animator.SetBool("IsTalk", true);
        Vector3 dir = playerController.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        rot.z = rot.x = 0;
        transform.DORotateQuaternion(rot, 1.0f);
        playerController.enabled = false;
        playerAttack.enabled = false;
        cameraController.enabled = false;
        textBGImage.SetActive(true);
    }

    protected virtual void StartDialogue()
    {

    }

    protected void ShowDialogueEntry()
    {
        
        if (currentDialogueIndex < dialogueEntries.Count)
        {
            DialogueEntry entry = dialogueEntries[currentDialogueIndex];
            characterName.text = entry.CharacterName;
            StartCoroutine(TypeSentence(entry.Text));
            currentDialogueIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    protected virtual void EndDialogue()
    {
        textBGImage.SetActive(false);
        animator.SetBool("IsTalk", false);
        playerController.enabled = true;
        playerAttack.enabled = true;
        cameraController.enabled = true;
        dialogueIdx++;
        if (dialogueIdx >= eventList.Count)
        {
            dialogueIdx = eventList.Count - 1;
        }
        currentDialogueIndex = 0;
        transform.DORotateQuaternion(originRot, 1.0f);
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void QuestAdd()
    {
        my2DExcMark.SetActive(true);
        my2DQuesNpcMark.SetActive(false);
        my2DNPCMark.SetActive(false);
        my3DExcMark.SetActive(true);
        my3DQuesMark.SetActive(false);
    }

    public void QuestClear()
    {
        my2DExcMark.SetActive(false);
        my2DQuesNpcMark.SetActive(true);
        my2DNPCMark.SetActive(false);
        my3DExcMark.SetActive(false);
        my3DQuesMark.SetActive(true);
    }

    public void QuestEnd()
    {
        my2DExcMark.SetActive(false);
        my2DQuesNpcMark.SetActive(false);
        my2DNPCMark.SetActive(true);
        my3DExcMark.SetActive(false);
        my3DQuesMark.SetActive(false);
    }
}
