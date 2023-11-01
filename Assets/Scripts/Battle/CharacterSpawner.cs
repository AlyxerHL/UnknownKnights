using System;
using System.Linq;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public static readonly string GreenTeamTag = "GreenTeam";
    public static readonly string RedTeamTag = "RedTeam";

    [SerializeField]
    private SpawnData[] greenTeamCharacters;

    [SerializeField]
    private SpawnData[] redTeamCharacters;

    public CharacterTag[] GreenTeamCharacters { get; private set; }
    public CharacterTag[] RedTeamCharacters { get; private set; }

    private void Awake()
    {
        GreenTeamCharacters = greenTeamCharacters
            .Select((spawnData) => SpawnCharacter(spawnData, GreenTeamTag))
            .ToArray();

        RedTeamCharacters = redTeamCharacters
            .Select((spawnData) => SpawnCharacter(spawnData, RedTeamTag))
            .ToArray();
    }

    private CharacterTag SpawnCharacter(SpawnData spawnData, string tag)
    {
        var character = Instantiate(
            spawnData.Prefab,
            spawnData.Position,
            Quaternion.identity,
            transform
        );

        character.tag = tag;
        character.Health.OnDeath += BattleReferee.MakeDecision;
        return character;
    }

    [Serializable]
    public struct SpawnData
    {
        [field: SerializeField]
        public CharacterTag Prefab { get; set; }

        [field: SerializeField]
        public Vector3 Position { get; set; }
    }
}
