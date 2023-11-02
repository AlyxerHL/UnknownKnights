using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePage : MonoBehaviour
{
    [SerializeField]
    private SlotAvatar[] slotAvatarPrefabs;

    [SerializeField]
    private RectTransform slotAvatarContainer;

    [SerializeField]
    private TextMeshProUGUI selectedCharacterNames;

    [SerializeField]
    private BattleGrid greenTeamBattleGrid;

    [SerializeField]
    private BattleGrid redTeamBattleGrid;

    private IEnumerable<SlotAvatar> slotAvatars;
    private readonly List<Character> selectedCharacters = new(3);

    private void Awake()
    {
        slotAvatars = slotAvatarPrefabs.Select(
            (prefab) => Instantiate(prefab, slotAvatarContainer)
        );

        foreach (var slotAvatar in slotAvatars)
        {
            slotAvatar.Selected += (character) =>
            {
                if (selectedCharacters.Contains(character))
                {
                    selectedCharacters.Remove(character);
                    UpdateSelectedCharacterNames();
                }
                else if (selectedCharacters.Count < 3)
                {
                    selectedCharacters.Add(character);
                    UpdateSelectedCharacterNames();
                }
            };
        }
    }

    public void StartBattle()
    {
        greenTeamBattleGrid.Top = selectedCharacters.ElementAtOrDefault(0);
        greenTeamBattleGrid.Bottom = selectedCharacters.ElementAtOrDefault(1);
        greenTeamBattleGrid.Back = selectedCharacters.ElementAtOrDefault(2);

        var randomCharacters = slotAvatars
            .Select((slotAvatar) => slotAvatar.CharacterPrefab)
            .OrderBy((_) => Random.value);

        redTeamBattleGrid.Top = randomCharacters.ElementAtOrDefault(0);
        redTeamBattleGrid.Bottom = randomCharacters.ElementAtOrDefault(1);
        redTeamBattleGrid.Back = randomCharacters.ElementAtOrDefault(2);

        SceneManager.LoadScene(1);
    }

    private void UpdateSelectedCharacterNames()
    {
        selectedCharacterNames.text = string.Join(
            "\n",
            selectedCharacters.Select((character) => character != null ? character.name : null)
        );
    }
}
