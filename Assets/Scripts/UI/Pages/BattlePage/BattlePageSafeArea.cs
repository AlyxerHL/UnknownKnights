using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BattlePageSafeArea : MonoBehaviour
{
    [SerializeField]
    private CharacterSpawner characterSpawner;

    private readonly List<Skill> greenTeamSkills = new();
    private bool isAutoSkillEnabled = true;

    private void Start()
    {
        characterSpawner.CharacterSpawned += (character) =>
        {
            if (character.CompareTag(CharacterSpawner.GreenTeamTag))
            {
                greenTeamSkills.Add(character.Skill);
            }
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
