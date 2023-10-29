using Slash.Unity.DataBind.Core.Data;

public class Scoreboard : Context
{
    private readonly Property<float> greenTeamScore = new(0.7f);
    private readonly Property<float> redTeamScore = new(0.3f);
    private readonly Property<int> timeInSeconds = new(1234);

    public Scoreboard()
    {
        TimeManager.OnTimeChanged += (time) => timeInSeconds.Value = time;
    }

    public float GreenTeamScore => greenTeamScore.Value;
    public float RedTeamScore => redTeamScore.Value;
    public int TimeInSeconds => timeInSeconds.Value;
}
