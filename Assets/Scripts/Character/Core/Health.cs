using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private UnityEvent onDeath;

    public event UnityAction OnDeath
    {
        add => onDeath.AddListener(value);
        remove => onDeath.RemoveListener(value);
    }

    public float CurrentHealth { get; set; }
    public float DamageReduction { get; set; } = 1f;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void GetDamaged(float amount)
    {
        CurrentHealth -= amount * DamageReduction;
        if (CurrentHealth <= 0f)
        {
            onDeath?.Invoke();
        }
    }

    public void GetHealed(float amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
    }
}
