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
        // Canvas 생성 및 설정
        GameObject canvasObject = new GameObject("LoadingCanvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1000;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();
        DontDestroyOnLoad(canvasObject);

        // 로딩 창 생성 및 설정
        loadingWindow = Instantiate(loadingWindowPrefab, canvasObject.transform);
        DontDestroyOnLoad(loadingWindow);
        loadingBar = loadingWindow.transform.GetChild(0).GetComponent<Image>();
        loadingWindow.SetActive(false);
        // 신이동 시 나올 텍스트와 이미지애니메이션
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

        // 첫 번째 씬을 비동기적으로 로드
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(sceneIdx, LoadSceneMode.Single);
        while (!sceneLoad.isDone)
        {
            loadingBar.fillAmount = Mathf.Clamp01(sceneLoad.progress / 0.9f);
            yield return null;
        }

        // PlayerScene을 비동기적으로 로드
        AsyncOperation playerLoad = SceneManager.LoadSceneAsync("PlayerScene", LoadSceneMode.Additive);
        while (!playerLoad.isDone)
        {
            loadingBar.fillAmount = Mathf.Clamp01(playerLoad.progress / 0.9f);
            yield return null;
        }

        RenderSettings.skybox = skybox;
        loadingWindow.SetActive(false);

        // PlayerScene을 활성화
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
        // 초기화 메서드 호출
        InitializeNPCDialogue();
        InitializeMutantAI();
        QuestManager.questInst.QuestSearch();
        SkillManager.skillInst.Initialize();
        // PlayerStartPoint 초기화
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
            case 2: sceneName = "호크반제국";
                SoundManager.soundInst.BackGroundMusic(0);
                break;
            case 3: sceneName = "마의접경지";
                SoundManager.soundInst.BackGroundMusic(1); break;
            case 4: sceneName = "뼈의무덤";
                SoundManager.soundInst.BackGroundMusic(2); break;
            case 5: sceneName = "하급마족의 마을";
                SoundManager.soundInst.BackGroundMusic(3); break;
            case 6: sceneName = "중급마족 성";
                SoundManager.soundInst.BackGroundMusic(4); break;
            case 7: sceneName = "마왕의 성";
                SoundManager.soundInst.BackGroundMusic(5); break;
        }
        // Scene Move Start 애니메이션
        sceneMoveStart.gameObject.SetActive(true);
        sceneMoveStart.transform.GetChild(0).GetComponent<Text>().text = sceneName;

        RectTransform scmTr = sceneMoveStart.transform.GetChild(1).GetComponent<RectTransform>();
        scmTr.anchoredPosition = new Vector2(-120, scmTr.anchoredPosition.y); // 초기 위치 설정

        scmTr.DOAnchorPosX(120, 2).OnComplete(() =>
        {
            sceneMoveStart.GetComponent<CanvasGroup>().DOFade(0, 2.0f).OnComplete(() =>
            {
                scmTr.anchoredPosition = new Vector2(-120, scmTr.anchoredPosition.y); // 초기 위치로 되돌림
                sceneMoveStart.gameObject.SetActive(false);
                sceneMoveStart.GetComponent<CanvasGroup>().alpha = 1; // alpha 값 초기화
            });
        });
        ItemManager.itemInst.Initialize();
        // 게임 데이터를 업데이트하여 저장
        DataManager.dataInst.DataSave();
    }
    private void InitializeNPCDialogue()
    { // 씬에있는 NPCDialouge들을 초기화 해준다
        NPCDialogue[] dialogues = FindObjectsOfType<NPCDialogue>();
        foreach (var dialogue in dialogues) { dialogue.Initialize(); }
    }

    private void InitializeMutantAI()
    { // 신에 있는 MutatnAi들을 초기화 해준다
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
