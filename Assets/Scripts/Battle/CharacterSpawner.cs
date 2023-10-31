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
        character.Health.OnDeath += DetermineBattleStatus;
    }

    private void DetermineBattleStatus()
    {
        if (CharacterTag.ActiveTags.Count == 0)
        {
            Debug.Log("Is this even possible?");
            return;
        }

        var firstTag = CharacterTag.ActiveTags.First().tag;
        var isFinished = CharacterTag.ActiveTags.All((tag) => tag.CompareTag(firstTag));

        if (isFinished)
        {
            Debug.Log(firstTag + " Win!");
        }
    }

    [Serializable]
    private struct SpawnData
    {
        [field: SerializeField]
        public CharacterTag Prefab { get; set; }

        [field: SerializeField]
        public Vector3 Position { get; set; }
    }
}
