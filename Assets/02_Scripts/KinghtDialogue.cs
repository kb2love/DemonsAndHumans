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
            // "기사단장과의 만남" 이벤트에 해당하는 대화 목록을 가져옵니다.
            List<DialogueEntry> dialogueEntries = DialogueManager.instance.GetDialogueByEvent("기사단장과의 만남");

            // 대화 목록이 존재하는지 확인합니다.
            if (dialogueEntries != null)
            {
                // 첫 번째 대화 항목을 가져옵니다.
                DialogueEntry entry = dialogueEntries[0];

                // 캐릭터 이름과 대화 내용을 TextMeshProUGUI 컴포넌트에 표시합니다.
                characterName.text = entry.CharacterName;
                dialogueText.text = entry.Text;
            }
            else
            {
                Debug.LogWarning("이벤트 '기사단장과의 만남'에 해당하는 대화 항목을 찾을 수 없습니다.");
            }
        }
    }
}
