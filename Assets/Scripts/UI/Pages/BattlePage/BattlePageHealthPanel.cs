using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public class BattlePageHealthPanel : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBarPrefab;

    [SerializeField]
    private DamageView damageViewPrefab;

    [SerializeField]
    private CharacterSpawner characterSpawner;

    private ObjectPool<DamageView> damageViewPool;

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

                var damageAmount = Mathf.RoundToInt(-amount);
                var damageView = damageViewPool.Get();
                damageView.Show(character.transform, damageAmount).Forget();
            };
        };

        damageViewPool = new(
            () =>
            {
                var damageView = Instantiate(damageViewPrefab, transform);
                damageView.PoolToReturn = damageViewPool;
                return damageView;
            },
            (damageView) => damageView.gameObject.SetActive(true),
            (damageView) => damageView.gameObject.SetActive(false)
        );
    }
}
