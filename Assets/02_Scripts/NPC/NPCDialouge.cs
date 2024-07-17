using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f; // 한 글자씩 나타나는 속도
    [SerializeField] protected List<string> eventList = new List<string>();
    [Header("3D My Mark")]
    [SerializeField] protected GameObject my3DExcMark;  // 나의 3D !마크
    [SerializeField] protected GameObject my3DQuesMark; // 나의 3D ? 마크
    [Header("2D My Mark")]
    [SerializeField] protected GameObject my2DExcMark;  // 나의 2D ! 마크
    [SerializeField] protected GameObject my2DQuesNpcMark;//나의 2D ? 마크
    [SerializeField] protected GameObject my2DNPCMark;  // 나의 동그라미
    protected GameObject textBGImage;
    protected Text dialogueText;
    protected Text characterName;
    protected Animator animator;
    protected PlayerAttack playerAttack;
    protected PlayerController playerController;
    protected CameraController cameraController;
    protected List<DialogueEntry> dialogueEntries; // 현재 이벤트의 대화 목록을 저장
    protected int currentDialogueIndex; // 현재 대화 인덱스를 저장
    protected int dialogueIdx = 0; // protected로 유지하여 파생 클래스에서 접근 가능하게 함
    private Quaternion originRot;

    protected virtual void Start()
    {
        textBGImage = GameObject.Find("Canvas_Player").transform.GetChild(0).gameObject;
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
        // 이 메서드는 자식 클래스에서 오버라이드할 수 있습니다.
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
    // 내 마크 제거만 존재해야한다 즉 생성하는것도 내것만 생성하고 지우는것도 내것만 지워야한다
    // 그러면 뭘 만들어야하나 경우의수 1. 내 퀘스트가 생겼을때 즉 ! 2. 내 퀘스트가 클리어 됐을때 즉 ? 3. 퀘스트를 끝마치고 없어졌을때 NPC마크
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
