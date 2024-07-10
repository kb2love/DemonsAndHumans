using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f; // �� ���ھ� ��Ÿ���� �ӵ�
    [SerializeField] protected List<string> eventList = new List<string>();
    [SerializeField] protected GameObject my3DExcMark;
    [SerializeField] protected GameObject my3DQuesMark;
    [SerializeField] protected GameObject my2DExcMark;
    [SerializeField] protected GameObject my2DQuesNpcMark;
    [SerializeField] protected GameObject my2DNPCMark;
    [Header("NextNPCMark")]
    [SerializeField] protected GameObject next3DExcMark;
    [SerializeField] protected GameObject next3DQuesMark;
    [SerializeField] protected GameObject next2DExcMark;
    [SerializeField] protected GameObject next2DQuesNpcMark;

    protected GameObject textBGImage;
    protected Text dialogueText;
    protected Text characterName;
    protected Animator animator;
    protected PlayerController playerController;
    protected CameraController cameraController;
    protected List<DialogueEntry> dialogueEntries; // ���� �̺�Ʈ�� ��ȭ ����� ����
    protected int currentDialogueIndex; // ���� ��ȭ �ε����� ����
    protected int dialogueIdx = 0; // protected�� �����Ͽ� �Ļ� Ŭ�������� ���� �����ϰ� ��
    private Quaternion originRot;

    protected virtual void Start()
    {
        textBGImage = GameObject.Find("Canvas_Player").transform.GetChild(0).gameObject;
        dialogueText = textBGImage.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        characterName = textBGImage.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        cameraController = Camera.main.transform.parent.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        originRot = transform.rotation;
    }

    public virtual void TextBox()
    {
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
        cameraController.enabled = false;
        textBGImage.SetActive(true);
        StartDialogue();
    }
    protected virtual void StartDialogue()
    {
        // �� �޼���� �ڽ� Ŭ�������� �������̵��� �� �ֽ��ϴ�.
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
        cameraController.enabled = true;
        dialogueIdx++;
        if (dialogueIdx >= eventList.Count)
        {
            dialogueIdx = eventList.Count - 1;
        }
        currentDialogueIndex = 0;
        transform.DORotateQuaternion(originRot, 1.0f);
        if (my3DQuesMark != null && !my3DQuesMark.activeSelf)
        {
            my3DQuesMark.SetActive(true);
        }
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
}
