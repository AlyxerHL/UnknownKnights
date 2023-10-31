using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class BattleScene : MonoBehaviour
{
    [SerializeField]
    private int timeLimit;

    [SerializeField]
    private CharacterSpawnData[] charactersSpawnData;

    private int timeLeft;

    public static BattleScene Instance { get; private set; }

    public event Action<int> OnTimeChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        charactersSpawnData.ForEach(SpawnCharacter);

        timeLeft = timeLimit;
        OnTimeChanged?.Invoke(timeLeft);

        DOTween
            .To(() => timeLeft, (x) => timeLeft = x, 0, timeLimit)
            .SetEase(Ease.Linear)
            .OnUpdate(() => OnTimeChanged?.Invoke(timeLeft))
            .OnComplete(() => Debug.Log("Time's up!"));
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
