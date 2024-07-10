using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMove : MonoBehaviour
{
    public static SceneMove SceneInst;
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
        SceneManager.LoadScene(2);
        SceneManager.LoadScene("PlayerScene", LoadSceneMode.Additive);
    }
    public void MutantFieldScene()
    {
        SceneManager.LoadScene(3);
        SceneManager.LoadScene("PlayerScene", LoadSceneMode.Additive);
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
