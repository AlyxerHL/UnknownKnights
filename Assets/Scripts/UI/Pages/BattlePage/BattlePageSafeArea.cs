using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BattlePageSafeArea : MonoBehaviour
{
    [SerializeField]
    private CharacterSpawner characterSpawner;

    [SerializeField]
    private SkillAvatar skillAvatarPrefab;

    [SerializeField]
    private RectTransform skillAvatarContainer;

    private readonly List<Skill> greenTeamSkills = new();
    private bool isAutoSkillEnabled = true;
    private bool isTimeAccelerated = false;

    private void Awake()
    {
        characterSpawner.CharacterSpawned += (character) =>
        {
            if (!character.CompareTag(CharacterSpawner.GreenTeamTag))
            {
                return;
            }

            greenTeamSkills.Add(character.Skill);
            var sprite = character.GetComponent<SpriteRenderer>().sprite;
            var skillAvatar = Instantiate(skillAvatarPrefab, skillAvatarContainer);
            skillAvatar.Initialize(sprite, character.Skill, character.Health);
        };
    }

    public void Pause()
    {
        PagesRouter.GoTo("PausePage").Forget();
    }

    public void ToggleAutoSkill()
    {
        Action<Skill> action = isAutoSkillEnabled
            ? (skill) => skill.StopAutoSkill()
            : (skill) => skill.StartAutoSkill().Forget();
        greenTeamSkills.ForEach(action);
        isAutoSkillEnabled = !isAutoSkillEnabled;
    }

    public void ToggleGameSpeed()
    {
        TimeSystem.BaseTimeScale = isTimeAccelerated ? 1f : 1.5f;
        TimeSystem.ResetTimeScale();
        isTimeAccelerated = !isTimeAccelerated;
    }
}
