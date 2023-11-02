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

    private void Start()
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
        if (isAutoSkillEnabled)
        {
            greenTeamSkills.ForEach((skill) => skill.StopAutoSkill());
        }
        else
        {
            greenTeamSkills.ForEach((skill) => skill.StartAutoSkill().Forget());
        }

        isAutoSkillEnabled = !isAutoSkillEnabled;
    }

    public void ToggleGameSpeed()
    {
        Debug.Log("ToggleGameSpeed");
    }
}
