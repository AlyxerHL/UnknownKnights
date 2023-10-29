using System;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private Character[] characters;

    private void Start()
    {
        characters.ForEach(InitializeCharacter);
    }

    private void InitializeCharacter(Character ch)
    {
        var gameObject = Instantiate(ch.Prefab, ch.Position, Quaternion.identity, transform);
        gameObject.tag = ch.Team + nameof(Team);
    }

    public enum Team
    {
        Green,
        Red
    }

    [Serializable]
    private struct Character
    {
        [field: SerializeField]
        public Team Team { get; set; }

        [field: SerializeField]
        public CharacterTag Prefab { get; set; }

        [field: SerializeField]
        public Vector3 Position { get; set; }
    }
}
