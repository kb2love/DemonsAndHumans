using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class SceneMove : MonoBehaviour
{
    public static SceneMove SceneInst;
    [SerializeField] Material castleSkyBox;
    [SerializeField] Material mutantFieldSkyBox;
    [SerializeField] GameObject loadingWindowPrefab;
    [SerializeField] GameObject scnemoveStartPrefab;
    [SerializeField] PlayerData playerData;
    private GameObject loadingWindow;
    private GameObject sceneMoveStart;
    private Image loadingBar;
    public int currentScene => _currentScene;
    private int _currentScene;
    string sceneName;
    private void Awake()
    {
        if (SceneInst == null)
        {
            SceneInst = this;
            DontDestroyOnLoad(gameObject);
            InitializeLoadingUI();
        }
        else if (SceneInst != this) { Destroy(gameObject); }
    }

    private void InitializeLoadingUI()
    {
        // Canvas ���� �� ����
        GameObject canvasObject = new GameObject("LoadingCanvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1000;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();
        DontDestroyOnLoad(canvasObject);

        // �ε� â ���� �� ����
        loadingWindow = Instantiate(loadingWindowPrefab, canvasObject.transform);
        DontDestroyOnLoad(loadingWindow);
        loadingBar = loadingWindow.transform.GetChild(0).GetComponent<Image>();
        loadingWindow.SetActive(false);
        // ���̵� �� ���� �ؽ�Ʈ�� �̹����ִϸ��̼�
        sceneMoveStart = Instantiate(scnemoveStartPrefab, canvasObject.transform);
        DontDestroyOnLoad(sceneMoveStart);
        sceneMoveStart.SetActive(false);

    }

    public void CatleScene()
    {
        StartCoroutine(LoadSceneAsync(2, castleSkyBox, resetGame: true));
        
    }

    public void PotalMove(int sceneIdx)
    {
        Material material = mutantFieldSkyBox;
        if (sceneIdx == 2) material = castleSkyBox;
        StartCoroutine(LoadSceneAsync(sceneIdx, material));
    }

    public void LoadScene(int sceneIdx, GameData gameData)
    {
        Material material = mutantFieldSkyBox;
        if (sceneIdx == 2) material = castleSkyBox;
        StartCoroutine(LoadSceneAsync(sceneIdx, material, gameData));
    }

    private IEnumerator LoadSceneAsync(int sceneIdx, Material skybox, GameData gameData = null, bool resetGame = false)
    {
        loadingWindow.SetActive(true);
        loadingBar.fillAmount = 0f;

        // ù ��° ���� �񵿱������� �ε�
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(sceneIdx, LoadSceneMode.Single);
        while (!sceneLoad.isDone)
        {
            loadingBar.fillAmount = Mathf.Clamp01(sceneLoad.progress / 0.9f);
            yield return null;
        }

        // PlayerScene�� �񵿱������� �ε�
        AsyncOperation playerLoad = SceneManager.LoadSceneAsync("PlayerScene", LoadSceneMode.Additive);
        while (!playerLoad.isDone)
        {
            loadingBar.fillAmount = Mathf.Clamp01(playerLoad.progress / 0.9f);
            yield return null;
        }

        RenderSettings.skybox = skybox;
        loadingWindow.SetActive(false);

        // PlayerScene�� Ȱ��ȭ
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("PlayerScene"));

        if (resetGame)
        {
            QuestManager.questInst.QuestReset();
            DataManager.dataInst.DeleteSaveData();
            ItemManager.itemInst.ReSetGame();
        }
        else { DataManager.dataInst.DataLoad(); }
        DialogueManager.dialogueInst.Initialize();
        ObjectPoolingManager.objInst.Initialize();
        GameManager.GM.Initialize();
        // �ʱ�ȭ �޼��� ȣ��
        InitializeNPCDialogue();
        InitializeMutantAI();
        QuestManager.questInst.QuestSearch();
        SkillManager.skillInst.Initialize();
        // PlayerStartPoint �ʱ�ȭ
        PlayerStartPoint playerStartPoint = FindObjectOfType<PlayerStartPoint>();
        if (gameData != null && gameData.playerPosition != null && gameData.playerRotation != null)
        {
            Transform playerTransform = playerStartPoint.transform.GetChild(playerData.playerSceneIdx).transform;
            playerTransform.position = new Vector3(gameData.playerPosition[0], gameData.playerPosition[1], gameData.playerPosition[2]);
            playerTransform.rotation = Quaternion.Euler(gameData.playerRotation[0], gameData.playerRotation[1], gameData.playerRotation[2]);
        }
        playerStartPoint.Initialize();
        _currentScene = sceneIdx;
        switch (sceneIdx)
        {
            case 2: sceneName = "ȣũ������";
                SoundManager.soundInst.BackGroundMusic(0);
                break;
            case 3: sceneName = "����������";
                SoundManager.soundInst.BackGroundMusic(1); break;
            case 4: sceneName = "���ǹ���";
                SoundManager.soundInst.BackGroundMusic(2); break;
            case 5: sceneName = "�ϱ޸����� ����";
                SoundManager.soundInst.BackGroundMusic(3); break;
            case 6: sceneName = "�߱޸��� ��";
                SoundManager.soundInst.BackGroundMusic(4); break;
            case 7: sceneName = "������ ��";
                SoundManager.soundInst.BackGroundMusic(5); break;
        }
        // Scene Move Start �ִϸ��̼�
        sceneMoveStart.gameObject.SetActive(true);
        sceneMoveStart.transform.GetChild(0).GetComponent<Text>().text = sceneName;

        RectTransform scmTr = sceneMoveStart.transform.GetChild(1).GetComponent<RectTransform>();
        scmTr.anchoredPosition = new Vector2(-120, scmTr.anchoredPosition.y); // �ʱ� ��ġ ����

        scmTr.DOAnchorPosX(120, 2).OnComplete(() =>
        {
            sceneMoveStart.GetComponent<CanvasGroup>().DOFade(0, 2.0f).OnComplete(() =>
            {
                scmTr.anchoredPosition = new Vector2(-120, scmTr.anchoredPosition.y); // �ʱ� ��ġ�� �ǵ���
                sceneMoveStart.gameObject.SetActive(false);
                sceneMoveStart.GetComponent<CanvasGroup>().alpha = 1; // alpha �� �ʱ�ȭ
            });
        });
        ItemManager.itemInst.Initialize();
        // ���� �����͸� ������Ʈ�Ͽ� ����
        DataManager.dataInst.DataSave();
    }
    private void InitializeNPCDialogue()
    { // �����ִ� NPCDialouge���� �ʱ�ȭ ���ش�
        NPCDialogue[] dialogues = FindObjectsOfType<NPCDialogue>();
        foreach (var dialogue in dialogues) { dialogue.Initialize(); }
    }

    private void InitializeMutantAI()
    { // �ſ� �ִ� MutatnAi���� �ʱ�ȭ ���ش�
        MutantAI[] mutantAI = FindObjectsOfType<MutantAI>();
        foreach (var ai in mutantAI) { ai.Initialize(); }
        MutantBoss mutantBoss = FindAnyObjectByType<MutantBoss>();
        if (mutantBoss != null) { mutantBoss.Initialize(); }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
