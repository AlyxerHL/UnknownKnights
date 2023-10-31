using System;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private CharacterSpawnData[] charactersSpawnData;

    private void Start()
    {
        charactersSpawnData.ForEach(SpawnCharacter);
    }

    private void SpawnCharacter(CharacterSpawnData data)
    {
        var character = Instantiate(data.Prefab, data.Position, Quaternion.identity, transform);
        character.tag = data.Team.ToString();
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

    public enum Team
    {
        Green,
        Red
    }

    [Serializable]
    private struct CharacterSpawnData
    {
        [field: SerializeField]
        public Team Team { get; set; }

        [field: SerializeField]
        public CharacterTag Prefab { get; set; }

        [field: SerializeField]
        public Vector3 Position { get; set; }
    }
}
