using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] private TextAsset dialogueData; // ��ȭ �����͸� ���� �ؽ�Ʈ ����

    // ��ȭ �����͸� ������ ��ųʸ�
    private Dictionary<string, List<DialogueEntry>> dialogueDictionary = new Dictionary<string, List<DialogueEntry>>();

    // Awake���� ��ȭ ������ �ε�
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            LoadDialogueData();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadDialogueData()
    {
        string[] lines = dialogueData.text.Split('\n');

        foreach (string line in lines)
        {
            string[] parts = line.Split(new char[] { ',' }, 3); // �ִ� 3���� �и��Ͽ� ��翡 ��ǥ�� ���Ե� �� �ְ� ��
            if (parts.Length >= 3)
            {
                string eventName = parts[0].Trim();
                string characterName = parts[1].Trim();
                string text = parts[2].Trim();

                DialogueEntry entry = new DialogueEntry(characterName, text);

                if (!dialogueDictionary.ContainsKey(eventName))
                {
                    dialogueDictionary[eventName] = new List<DialogueEntry>();
                }
                dialogueDictionary[eventName].Add(entry);

            }
        }
    }

    // �̺�Ʈ �̸����� ��ȭ ������ ��������
    public List<DialogueEntry> GetDialogueByEvent(string eventName)
    {
        if (dialogueDictionary.ContainsKey(eventName))
        {
            return dialogueDictionary[eventName];
        }
        return null;
    }
}

public class DialogueEntry
{
    public string CharacterName;
    public string Text;

    public DialogueEntry(string characterName, string text)
    {
        CharacterName = characterName;
        Text = text;
    }
}
