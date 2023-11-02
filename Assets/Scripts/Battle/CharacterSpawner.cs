using System;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public static readonly string GreenTeamTag = "GreenTeam";
    public static readonly string RedTeamTag = "RedTeam";

    [SerializeField]
    private BattleGrid greenTeamGrid;

    [SerializeField]
    private BattleGrid redTeamGrid;

    public event Action<Character> CharacterSpawned;

    private void Start()
    {
        SpawnCharacter(greenTeamGrid.Top, BattleGrid.TopPosition, GreenTeamTag);
        SpawnCharacter(greenTeamGrid.Bottom, BattleGrid.BottomPosition, GreenTeamTag);
        SpawnCharacter(greenTeamGrid.Back, BattleGrid.BackPosition, GreenTeamTag);
        SpawnCharacter(redTeamGrid.Top, BattleGrid.TopPosition * -1f, RedTeamTag);
        SpawnCharacter(redTeamGrid.Bottom, BattleGrid.BottomPosition * -1f, RedTeamTag);
        SpawnCharacter(redTeamGrid.Back, BattleGrid.BackPosition * -1f, RedTeamTag);
    }

    private void SpawnCharacter(Character prefab, Vector2 position, string tag)
    {
        var character = Instantiate(prefab, position, Quaternion.identity, transform);
        character.tag = tag;
        character.Health.Dead += BattleReferee.MakeDecision;
        CharacterSpawned?.Invoke(character);
    }
}
