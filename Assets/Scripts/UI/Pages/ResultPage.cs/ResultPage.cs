using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultPage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI result;

    public void Initialize(BattleReferee.Result battleResult)
    {
        result.text = battleResult.ToString().ToUpper();
    }

    public void GoToHome()
    {
        BattleTime.ResumeTimeScale();
        Time.timeScale = 1f;
        DOTween.KillAll();
        SceneManager.LoadScene(0);
    }
}
