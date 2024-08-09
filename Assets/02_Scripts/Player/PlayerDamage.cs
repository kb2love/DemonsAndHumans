using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] Transform shieldTr;
    [SerializeField] Image plHp;
    [SerializeField] CanvasGroup fadeOutImage;
    [SerializeField] AudioClip audioClip;
    PlayerController playerController;
    AudioSource audioSource;
    Animator animator;

    GameObject hitEff;
    bool isShield;
    private bool isHit = false;
    public bool IsHit
    {
        get
        {
            return isHit;
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
    }
    public void HitDamage(float damage)
    {
        hitEff = ObjectPoolingManager.objInst.GetHitEff();
        animator.SetTrigger("HitTrigger");
        if (!isShield)
        {
            hitEff.transform.position = transform.position + (Vector3.up * 0.8f);
            damage = damage - (GameManager.GM.playerDataJson.DefenceValue / 2);
            if (damage <= 0) damage = 1;
            GameManager.GM.playerDataJson.HP -= damage;
            plHp.fillAmount = GameManager.GM.playerDataJson.HP / GameManager.GM.playerDataJson.MaxHP;
            GameManager.GM.StatUpdate(PlayerStat.HP);
            DataManager.dataInst.PlayerDataSave(GameManager.GM.playerDataJson);
            if (GameManager.GM.playerDataJson.HP <= 0)
            {
                playerController.enabled = false;
                animator.SetTrigger("DieTrigger");
                GetComponent<PlayerAttack>().enabled = false;
                GetComponent<Collider>().enabled = false;
                GetComponent<PlayerUIController>().enabled = false;
                GameManager.GM.playerDataJson.currentSceneIdx = 0;
                fadeOutImage.DOFade(1, 3.0f).OnComplete(() => SceneMove.SceneInst.DeathScene());
            }
        }
        else
        {
            Debug.Log("막앗죠?");
            SoundManager.soundInst.EffectSoundPlay(audioSource, audioClip);
            hitEff.transform.position = shieldTr.position;
            isHit = true;
            Invoke("IsHitFalse", 0.5f);
        }
        hitEff.SetActive(true);
    }
    // 아이템 얻는 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            ItemManager.itemInst.GetItem(other.gameObject.GetComponent<ItemInfo>().type, other.GetComponent<ItemInfo>().goldValue);
            SoundManager.soundInst.EffectSoundPlay(playerData.ItemGetClip);
            other.gameObject.SetActive(false);
        }
    }

    public void IsShieldOnOff(bool _isShield)
    {
        isShield = _isShield;
    }
    private void IsHitFalse()
    {
        isHit = false;
    }
}
