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

    public event Action<CharacterTag> CharacterSpawned;

    private void Start()
    {
        greenTeamCharacters.ForEach((spawnData) => SpawnCharacter(spawnData, GreenTeamTag));
        redTeamCharacters.ForEach((spawnData) => SpawnCharacter(spawnData, RedTeamTag));
    }

    private void SpawnCharacter(SpawnData spawnData, string tag)
    {
        var character = Instantiate(
            spawnData.Prefab,
            spawnData.Position,
            Quaternion.identity,
            transform
        );

        character.tag = tag;
        character.Health.Dead += BattleReferee.MakeDecision;
        CharacterSpawned?.Invoke(character);
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
