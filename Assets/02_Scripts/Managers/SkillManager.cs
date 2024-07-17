using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager skillInst;
    [SerializeField] PlayerData playerData;
    [SerializeField] Transform iceSkillImage;
    [SerializeField] Transform fireSkillImage;
    [SerializeField] Transform electroSkillImage;
    [SerializeField] List<Image> iceSkillImageList = new List<Image>();
    [SerializeField] List<Image> fireSkillImageList = new List<Image>();
    [SerializeField] List<Image> electroSkillImageList = new List<Image>();
    public List<Image> skillList = new List<Image>();
    public List<Sprite> skillImageList = new List<Sprite>();
    [SerializeField] List<Image> lastList = new List<Image>();
    [SerializeField] Image lastImage;
    int levelSkillIdx;
    PlayerAniEvent aniEvent;
    bool isSelec = false;
    private void Awake()
    {
        skillInst = this;
    }
    private void Start()
    {
        for (int i = 1; i < iceSkillImage.childCount; i++)
        {
            iceSkillImageList.Add(iceSkillImage.GetChild(i).GetComponent<Image>());
        }
        for (int i = 1; i < fireSkillImage.childCount; i++)
        {
            fireSkillImageList.Add(fireSkillImage.GetChild(i).GetComponent<Image>());
        }
        for (int i = 1; i < electroSkillImage.childCount; i++)
        {
            electroSkillImageList.Add(electroSkillImage.GetChild(i).GetComponent<Image>());
        }
        aniEvent = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerAniEvent>();
    }
    public void Level5()
    {
        playerData.levelSkillPoint++;
        levelSkillIdx = 0;
        LevelSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, levelSkillIdx);
    }
    public void IceSpear()
    {
        playerData.level05SkillIdx = Level05Skill(fireSkillImageList, electroSkillImageList, iceSkillImageList, levelSkillIdx, 1);
    }
    public void FireBall()
    {
        playerData.level05SkillIdx = Level05Skill(iceSkillImageList, electroSkillImageList, fireSkillImageList, levelSkillIdx, 2);
    }
    public void ElectroBall()
    {
        playerData.level05SkillIdx = Level05Skill(fireSkillImageList, iceSkillImageList, electroSkillImageList, levelSkillIdx, 3);
    }
    int Level05Skill(List<Image> firstList, List<Image> secondList, List<Image> thirdList, int idx, int playerSkillIdx)
    {
        firstList[idx].color = new Color(1, 1, 1, 0.4f);
        secondList[idx].color = new Color(1, 1, 1, 0.4f);
        firstList[idx].GetComponent<Button>().enabled = false;
        secondList[idx].GetComponent<Button>().enabled = false;
        isSelec = false;
        lastList = thirdList;
        thirdList[idx].GetComponent<Button>().onClick.AddListener(SkillSelec);
        aniEvent.Level05SkillChange(playerSkillIdx);
        playerData.levelSkillPoint--;
        return playerSkillIdx;
    }
    void LevelSkill(List<Image> iceList, List<Image> fireList, List<Image> electroList, int idx)
    {
        iceList[idx].color = new Color(1, 1, 1, 1);
        fireList[idx].color = new Color(1, 1, 1, 1);
        electroList[idx].color = new Color(1, 1, 1, 1);
        iceList[idx].GetComponent<Button>().enabled = true;
        fireList[idx].GetComponent<Button>().enabled = true;
        electroList[idx].GetComponent<Button>().enabled = true;
    }
    public void SkillSelec()
    {
        StartCoroutine(SkillSelecButton(playerData.level05SkillIdx - 1));
        // 스타트 코루틴을 시작하는동안 추가 코루틴을 발생시키지 않도록 버튼에 코루틴 추가 리스너를 삭제한다
        lastList[levelSkillIdx].GetComponent<Button>().onClick.RemoveListener(SkillSelec);
    }
    IEnumerator SkillSelecButton(int skillSelecIdx)
    {
        while(!isSelec)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Selec(0, skillSelecIdx);
                break;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Selec(1, skillSelecIdx);
                break;
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Selec(2, skillSelecIdx);
                break;
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                Selec(3, skillSelecIdx);
                break;
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                Selec(4, skillSelecIdx);
                break;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Selec(5, skillSelecIdx);
                break;
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Selec(6, skillSelecIdx);
                break;
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                Selec(7, skillSelecIdx);
                break;
            }
        }
        
    }

    private void Selec(int skillListIdx ,int skillSelecIdx)
    {   // 스킬을 이미 배치해논적이 있다면 그전에 배치했던곳은 빈칸으로 채운다
        if (lastImage != null)
        {
            lastImage.sprite = skillImageList[15];
        }
        skillList[skillListIdx].sprite = skillImageList[skillSelecIdx];
        lastImage = skillList[skillListIdx];
        lastList[levelSkillIdx].GetComponent<Button>().onClick.AddListener(SkillSelec);
        isSelec = true;
    }


}
