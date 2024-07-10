using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip; // 설명 텍스트 오브젝트
    public string itemName;
    public string itemExplain;
    public Text toolTipName;
    public Text toolTipExplain;
    Vector2 pos;
    Vector2 mousePosition;
    public void OnPointerEnter(PointerEventData eventData)
    {
        pos = GetComponent<RectTransform>().position;
        mousePosition = Input.mousePosition;
        if ((pos.x - 40 < mousePosition.x && mousePosition.x < pos.x + 40) && (pos.y - 40 < mousePosition.y && mousePosition.y < pos.y + 40))
        {
            toolTipName.text = itemName;
            toolTipExplain.text = itemExplain;
            tooltip.transform.position = Input.mousePosition;
            tooltip.SetActive(true); // 마우스가 이미지 위에 있을 때 툴팁 활성화
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pos = GetComponent<RectTransform>().position;
        mousePosition = Input.mousePosition;
        if((pos.x + 40 < mousePosition.x || mousePosition.x < pos.x - 40) || (pos.y + 40 < mousePosition.y || mousePosition.y < pos.y - 40))
        {
            tooltip.SetActive(false);
        }
    }
    
}
