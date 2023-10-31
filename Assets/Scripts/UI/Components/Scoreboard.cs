using System.Linq;
using Slash.Unity.DataBind.Core.Data;

public class Scoreboard : Context
{
    private readonly Property<float> greenTeamScoreProperty = new(1f);
    private readonly Property<float> redTeamScoreProperty = new(1f);
    private readonly Property<int> timeInSecondsProperty = new();

    private float greenTeamTotalMaxHealth;
    private float greenTeamTotalHealth;
    private float redTeamTotalMaxHealth;
    private float redTeamTotalHealth;

    public Scoreboard()
    {
        BattleManager.OnInitialized += (characters) =>
        {
            var greenTeamCharacters = characters.Where(
                (ch) => ch.CompareTag(BattleManager.Team.Green.ToString())
            );

            var redTeamCharacters = characters.Where(
                (ch) => ch.CompareTag(BattleManager.Team.Red.ToString())
            );

            greenTeamTotalMaxHealth = greenTeamCharacters.Sum((ch) => ch.Health.MaxHealth);
            redTeamTotalMaxHealth = redTeamCharacters.Sum((ch) => ch.Health.MaxHealth);
            greenTeamTotalHealth = greenTeamTotalMaxHealth;
            redTeamTotalHealth = redTeamTotalMaxHealth;

            greenTeamCharacters.ForEach(
                (ch) =>
                    ch.Health.OnHealthChange += (changeAmount) =>
                    {
                        greenTeamTotalHealth += changeAmount;
                        GreenTeamScore = greenTeamTotalHealth / greenTeamTotalMaxHealth;
                    }
            );

            redTeamCharacters.ForEach(
                (ch) =>
                    ch.Health.OnHealthChange += (changeAmount) =>
                    {
                        redTeamTotalHealth += changeAmount;
                        RedTeamScore = redTeamTotalHealth / redTeamTotalMaxHealth;
                    }
            );
        };

        TimeManager.OnTimeChanged += (time) => TimeInSeconds = time;
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
