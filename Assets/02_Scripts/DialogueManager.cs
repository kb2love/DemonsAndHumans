using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] private TextAsset dialogueData; // 대화 데이터를 담은 텍스트 에셋

    // 대화 데이터를 저장할 딕셔너리
    private Dictionary<string, List<DialogueEntry>> dialogueDictionary = new Dictionary<string, List<DialogueEntry>>();


    // Awake에서 대화 데이터 로드
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

    // 대화 데이터 로드
    private void LoadDialogueData()
    {
        // 텍스트 에셋에서 대화 데이터를 읽어와서 dictionary에 저장하는 로직
        string[] lines = dialogueData.text.Split('\n');
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
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

    // 이벤트 이름으로 대화 데이터 가져오기
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
