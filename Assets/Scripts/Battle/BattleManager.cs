using System;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private Character[] friendlyCharacters;

    [SerializeField]
    private Character[] enemyCharacters;

    private void Start()
    {
        SpawnCharacters(friendlyCharacters);
        SpawnCharacters(enemyCharacters);
    }

    private void SpawnCharacters(Character[] characters)
    {
        characters.ForEach(
            (ch) => Instantiate(ch.Prefab, ch.Position, Quaternion.identity, transform)
        );
    }

    [Serializable]
    private struct Character
    {
        [field: SerializeField]
        public Vector3 Position { get; set; }

        [field: SerializeField]
        public GameObject Prefab { get; set; }
    }
}
