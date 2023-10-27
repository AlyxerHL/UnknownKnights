using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDeath;

    [SerializeField]
    private float health = 200f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            OnDeath?.Invoke();
        }
    }
}
