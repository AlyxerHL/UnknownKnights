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

    public float DamageRate { get; set; } = 1f;
    private float CurrentHealth { get; set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void GetDamaged(float amount)
    {
        CurrentHealth -= amount * DamageRate;
        Debug.Log($"{gameObject.name} health: {CurrentHealth}");
        if (CurrentHealth <= 0f)
        {
            onDeath?.Invoke();
        }
    }

    public void GetHealed(float amount)
    {
        CurrentHealth += amount;
        Debug.Log($"{gameObject.name} health: {CurrentHealth}");
    }
}
