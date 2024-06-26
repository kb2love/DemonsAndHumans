using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCPaladinDialouge : MonoBehaviour
{
    [SerializeField] float typingSpeed = 0.05f; // �� ���ھ� ��Ÿ���� �ӵ�
    [SerializeField] List<string> eventList = new List<string>();
    [SerializeField] GameObject mark01;
    [SerializeField] GameObject nextDoor;
    [SerializeField] Transform gate01;
    [SerializeField] Transform gate02;
    GameObject textBGImage;
    Text dialogueText;
    Text characterName;
    private Animator animator;
    private PlayerController playerController;
    private CameraController cameraController;
    private List<DialogueEntry> dialogueEntries; // ���� �̺�Ʈ�� ��ȭ ����� ����
    private int currentDialogueIndex; // ���� ��ȭ �ε����� ����
    private int dialogueIdx = 0;
    private Quaternion originRot;
    private bool isWeapon = false;
    private bool isGive = false;
    private void Start()
    {
        textBGImage = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        dialogueText = textBGImage.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        characterName = textBGImage.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        cameraController = Camera.main.transform.parent.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        originRot = transform.rotation;
    }
    public void TextBox()
    {
        // ��ȭ�� �ε���� �ʾҴٸ� �ε��մϴ�.
        for(int i = 0; i < eventList.Count; i++)
        {
            if(dialogueIdx == i)
            {
                DialougeOn(eventList[i]);
                break;
            }
        }
        // ���� ��ȭ�� ǥ���մϴ�.
        ShowDialogueEntry();
    }

    private void DialougeOn(string _eventName)
    {
        dialogueEntries = DialogueManager.instance.GetDialogueByEvent(_eventName);
        animator.SetBool("IsTalk", true);
        Vector3 dir = playerController.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        rot.z = rot.x = 0;
        transform.DORotateQuaternion(rot, 1.0f);
        playerController.enabled = false;
        cameraController.enabled = false;
        textBGImage.SetActive(true);
    }

    private void ShowDialogueEntry()
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
            textBGImage.SetActive(false);
            animator.SetBool("IsTalk", false);
            playerController.enabled = true;
            cameraController.enabled = true;
            dialogueIdx++;
            if (!isWeapon)
            {
                dialogueIdx = 1;
                if(!isGive)
                {
                    GameManager.GM.GetItem(ItemData.ItemType.Sword);
                    isGive = true;
                }
            }
            else
            {
                dialogueIdx = 3;
                Sequence seq = DOTween.Sequence();
                seq.Append(gate01.DOBlendableRotateBy(new Vector3(0, 90, 0), 5.0f));
                seq.Join(gate02.DOBlendableRotateBy(new Vector3(0, -90, 0), 5.0f));
                nextDoor.SetActive(true);
            }
            mark01.SetActive(false);
            currentDialogueIndex = 0;
            transform.DORotateQuaternion(originRot, 1.0f);
            // ��ȭ�� ������ �� �ʿ��� �߰� �۾��� ���⿡ �߰�
        }
    }

    public void IsWeapon(bool _isWeapom)
    {
        isWeapon = _isWeapom;
        dialogueIdx++;
    }
    // �ؽ�Ʈ�� �� ���ھ� ����ϴ� �ڷ�ƾ
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
