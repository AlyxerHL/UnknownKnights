using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BattlePageSkillVfx : MonoBehaviour
{
    [SerializeField]
    private CharacterSpawner characterSpawner;

    [SerializeField]
    private SkillQuote skillQuotePrefab;

    [SerializeField]
    private float quoteDuration;

    private readonly HashSet<SkillQuote> skillQuotes = new();

    private void Awake()
    {
        gameObject.SetActive(false);
        characterSpawner.CharacterSpawned += (character) =>
        {
            SkillQuote skillQuote = null;
            character.Skill.BeganUsing += async () => skillQuote = await Show(character.Skill);
            character.Skill.EndedUsing += () => Hide(skillQuote);
        };
    }

    private async UniTask<SkillQuote> Show(Skill skill)
    {
        var skillQuote = Instantiate(skillQuotePrefab, transform);
        skillQuote.Initialize(skill);
        skillQuotes.Add(skillQuote);
        gameObject.SetActive(true);

        await UniTask.WaitForSeconds(quoteDuration, ignoreTimeScale: true);
        return skillQuote;
    }

    private UniTask Hide(SkillQuote skillQuote)
    {
        if (skillQuote == null)
        {
            return UniTask.CompletedTask;
        }

        skillQuotes.Remove(skillQuote);
        Destroy(skillQuote.gameObject);

        if (skillQuotes.Count == 0)
        {
            gameObject.SetActive(false);
        }
        return UniTask.CompletedTask;
    }
}
