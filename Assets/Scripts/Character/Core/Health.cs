using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [field: SerializeField]
    public float MaxHealth { get; private set; }

    [SerializeField]
    private UnityEvent onDeath;

    [SerializeField]
    private UnityEvent<float> onHealthChanged;

    private float currentHealth;

    public event UnityAction OnDeath
    {
        add => onDeath.AddListener(value);
        remove => onDeath.RemoveListener(value);
    }

    public event UnityAction<float> OnHealthChanged
    {
        add => onHealthChanged.AddListener(value);
        remove => onHealthChanged.RemoveListener(value);
    }

    public float DamageReduction { get; set; } = 1f;

    private void Awake()
    {
        currentHealth = MaxHealth;
    }

    public void GetDamaged(float amount)
    {
        amount = Mathf.Min(currentHealth, amount * DamageReduction);
        currentHealth -= amount;
        onHealthChanged?.Invoke(-amount);

        if (currentHealth.Approximately(0f))
        {
            onDeath?.Invoke();
        }
    }

    public void GetHealed(float amount)
    {
        amount = Mathf.Min(MaxHealth - currentHealth, amount);
        currentHealth += amount;
        onHealthChanged?.Invoke(amount);
    }
}
