using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMove : MonoBehaviour
{
    public static SceneMove SceneInst;
    [SerializeField] Material castleSkyBox;
    [SerializeField] Material mutantFieldSkyBox;
    private void Awake()
    {
        if(SceneInst == null)
        {
            SceneInst = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (SceneInst != this)
        {
            Destroy(gameObject);
        }
    }
    public void CatleScene()
    {
        SceneManager.LoadScene("PlayerScene");
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        RenderSettings.skybox = castleSkyBox;
    }
    public void PotalMove(int sncenIdx)
    {
        SceneManager.LoadScene("PlayerScene");
        SceneManager.LoadScene(sncenIdx, LoadSceneMode.Additive);
        RenderSettings.skybox = mutantFieldSkyBox;
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // 유니티 에디터를 종료합니다.
#else
            Application.Quit(); // 어플리케이션을 종료합니다.
#endif
    }
}
