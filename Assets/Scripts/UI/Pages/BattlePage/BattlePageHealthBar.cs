using UnityEngine;

public class BattlePageHealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBarPrefab;

    public void Initialize(CharacterSpawner characterSpawner)
    {
        characterSpawner.OnCharacterSpawned += (character) =>
        {
            var healthBar = Instantiate(healthBarPrefab, transform);
            healthBar.Initialize(character.Health);
        };
    }
}
