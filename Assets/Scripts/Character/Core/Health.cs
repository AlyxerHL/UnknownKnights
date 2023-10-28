using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    private UnityEvent onDeath;

    public event UnityAction OnDeath
    {
        add => onDeath.AddListener(value);
        remove => onDeath.RemoveListener(value);
    }

    public void GetDamaged(float amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} health: {health}");
        if (health <= 0f)
        {
            onDeath?.Invoke();
        }
    }
}
