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
            healthBar.Initialize(character.Health, character.Effector);

            character.Health.Changed += (changeAmount) =>
            {
                if (changeAmount == 0f)
                {
                    return;
                }

                var amount = Mathf.RoundToInt(changeAmount);
                var damageView = damageViewPool.Get();
                damageView.Show(character.transform, amount);
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
