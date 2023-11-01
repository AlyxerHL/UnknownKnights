using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField]
    public float MaxHealth { get; private set; }

    public event Action Dead;
    public event Action<float> Changed;

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
        Changed?.Invoke(-amount);

        if (CurrentHealth.Approximately(0f))
        {
            gameObject.SetActive(false);
            Dead?.Invoke();
        }
    }

    public void GetHealed(float amount)
    {
        amount = Mathf.Min(MaxHealth - CurrentHealth, amount);
        CurrentHealth += amount;
        Changed?.Invoke(amount);
    }
}
