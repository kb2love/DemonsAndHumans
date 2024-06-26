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
    List<Image> lastList = new List<Image>();
    PlayerAniEvent aniEvent;
    bool isSelec = false;
    private void Awake()
    {
        if (skillInst == null)
            skillInst = this;
        else if (skillInst != this)
            Destroy(skillInst);
        DontDestroyOnLoad(skillInst);
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
        LevelSkill(iceSkillImageList, fireSkillImageList, electroSkillImageList, 0);
    }
    public void IceSpear()
    {
        playerData.level05SkillIdx = Level05Skill(fireSkillImageList, electroSkillImageList, iceSkillImageList, 0, 1);
    }
    public void FireBall()
    {
        playerData.level05SkillIdx = Level05Skill(iceSkillImageList, electroSkillImageList, fireSkillImageList, 0, 2);
    }
    public void ElectroBall()
    {
        playerData.level05SkillIdx = Level05Skill(fireSkillImageList, iceSkillImageList, electroSkillImageList, 0, 3);
    }
    int Level05Skill(List<Image> firstList, List<Image> secondList, List<Image> thirdList, int idx, int playerSkillIdx)
    {
        firstList[idx].color = new Color(1, 1, 1, 0.4f);
        secondList[idx].color = new Color(1, 1, 1, 0.4f);
        firstList[idx].GetComponent<Button>().enabled = false;
        secondList[idx].GetComponent<Button>().enabled = false;
        isSelec = false;
        thirdList[idx].GetComponent<Button>().onClick.AddListener(SkillSelec);
        lastList = thirdList;
        aniEvent.Level05SkillChange(playerSkillIdx);
        playerData.levelSkillPoint--;
        return playerSkillIdx;
    }
    public void SkillSelec()
    {
        StartCoroutine(SkillSelecButton(playerData.level05SkillIdx - 1));
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
    {
        skillList[skillListIdx].sprite = skillImageList[skillSelecIdx];
        lastList[0].GetComponent<Button>().enabled = false;
        isSelec = true;
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

}
