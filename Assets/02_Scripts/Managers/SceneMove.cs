using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMove : MonoBehaviour
{
    public static SceneMove SceneInst;
    private void Awake()
    {
        if (SceneInst == null)
            SceneInst = this;
        else if(SceneInst != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void CatleScene()
    {
        SceneManager.LoadScene(1);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }
    public void MutantFieldScene()
    {
        SceneManager.LoadScene(3);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }
}
