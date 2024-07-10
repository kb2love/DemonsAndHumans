using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip; // ���� �ؽ�Ʈ ������Ʈ
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
            tooltip.SetActive(true); // ���콺�� �̹��� ���� ���� �� ���� Ȱ��ȭ
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
