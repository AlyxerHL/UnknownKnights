using Cysharp.Threading.Tasks;
using UnityEngine;

public class BattlePageHealthPanel : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBarPrefab;

    [SerializeField]
    private DamageView damageViewPrefab;

    [SerializeField]
    private CharacterSpawner characterSpawner;

    public void Awake()
    {
        characterSpawner.CharacterSpawned += (character) =>
        {
            var healthBar = Instantiate(healthBarPrefab, transform);
            healthBar.Initialize(character.Health);

            character.Health.Changed += (amount) =>
            {
                if (amount >= 0)
                {
                    return;
                }

                amount = Mathf.RoundToInt(-amount);
                var damageView = Instantiate(damageViewPrefab, transform);
                damageView.Show(character.transform, amount).Forget();
            };
        };
    }
}
