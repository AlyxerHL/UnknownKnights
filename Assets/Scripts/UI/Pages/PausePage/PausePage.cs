using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePage : MonoBehaviour
{
    public void Resume()
    {
        Time.timeScale = 1f;
        PagesRouter.GoTo("BattlePage").Forget();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void Surrender()
    {
        Time.timeScale = 1f;
        PagesRouter.GoTo("HomePage").Forget();
    }
}
