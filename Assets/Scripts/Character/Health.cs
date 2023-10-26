using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health = 200f;

    public event Action OnDeath;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            OnDeath?.Invoke();
        }
    }
}
