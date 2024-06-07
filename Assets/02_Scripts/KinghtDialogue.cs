using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class KinghtDialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI characterName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // "��������� ����" �̺�Ʈ�� �ش��ϴ� ��ȭ ����� �����ɴϴ�.
            List<DialogueEntry> dialogueEntries = DialogueManager.instance.GetDialogueByEvent("��������� ����");

            // ��ȭ ����� �����ϴ��� Ȯ���մϴ�.
            if (dialogueEntries != null)
            {
                // ù ��° ��ȭ �׸��� �����ɴϴ�.
                DialogueEntry entry = dialogueEntries[0];

                // ĳ���� �̸��� ��ȭ ������ TextMeshProUGUI ������Ʈ�� ǥ���մϴ�.
                characterName.text = entry.CharacterName;
                dialogueText.text = entry.Text;
            }
            else
            {
                Debug.LogWarning("�̺�Ʈ '��������� ����'�� �ش��ϴ� ��ȭ �׸��� ã�� �� �����ϴ�.");
            }
        }
    }
}
