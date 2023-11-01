using UnityEngine;

public class BattlePageHealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBarPrefab;

    public void Initialize(CharacterSpawner characterSpawner)
    {
        characterSpawner.CharacterSpawned += (character) =>
        {
            var healthBar = Instantiate(healthBarPrefab, transform);
            healthBar.Initialize(character.Health);
        };
    }
}
