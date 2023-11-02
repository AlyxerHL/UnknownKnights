using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        BattleTime.ResumeTimeScale();
        DOTween.KillAll();
        SceneManager.LoadScene(1);
    }

    public void GoToHome()
    {
        Time.timeScale = 1f;
        BattleTime.ResumeTimeScale();
        DOTween.KillAll();
        SceneManager.LoadScene(0);
    }
}
