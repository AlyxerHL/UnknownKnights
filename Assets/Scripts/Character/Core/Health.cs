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

    public float CurrentHealth { get; private set; }
    public float DamageReduction { get; set; } = 1f;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void GetDamaged(float amount)
    {
        amount = Mathf.Min(CurrentHealth, amount * DamageReduction);
        CurrentHealth -= amount;
        onHealthChanged?.Invoke(-amount);

        if (CurrentHealth.Approximately(0f))
        {
            onDeath?.Invoke();
        }
    }

    public void GetHealed(float amount)
    {
        amount = Mathf.Min(MaxHealth - CurrentHealth, amount);
        CurrentHealth += amount;
        onHealthChanged?.Invoke(amount);
    }
}
