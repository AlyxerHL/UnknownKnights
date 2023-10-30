using Slash.Unity.DataBind.Core.Data;
using UnityEngine;

public class Scoreboard : Context
{
    private readonly Property<float> greenTeamScoreProperty = new(0.7f);
    private readonly Property<float> redTeamScoreProperty = new(0.3f);
    private readonly Property<int> timeInSecondsProperty = new(1234);

    public Scoreboard()
    {
        TimeManager.OnTimeChanged += (time) =>
        {
            TimeInSeconds = time;
            Debug.Log(time);
        };
    }

    public float GreenTeamScore
    {
        get => greenTeamScoreProperty.Value;
        set => greenTeamScoreProperty.Value = value;
    }

    public float RedTeamScore
    {
        get => redTeamScoreProperty.Value;
        set => redTeamScoreProperty.Value = value;
    }

    public int TimeInSeconds
    {
        get => timeInSecondsProperty.Value;
        set => timeInSecondsProperty.Value = value;
    }
}
