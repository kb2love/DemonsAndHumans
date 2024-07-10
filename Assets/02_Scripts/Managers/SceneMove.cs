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
        EditorApplication.isPlaying = false; // ����Ƽ �����͸� �����մϴ�.
#else
            Application.Quit(); // ���ø����̼��� �����մϴ�.
#endif
    }
}
