using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultPage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI result;

    public void Initialize(bool hasWin)
    {
        result.text = hasWin ? "VICTORY" : "DEFEAT";
    }

    public void GoToHome()
    {
        BattleTime.ResumeTimeScale();
        Time.timeScale = 1f;
        DOTween.KillAll();
        SceneManager.LoadScene(0);
    }
}
