using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    // THIS ADDS THE EVENT (Fixes your error)
    public Action onDeath;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Call the event so the spawner knows this enemy died
        if (onDeath != null)
            onDeath.Invoke();

        Destroy(gameObject);
    }
}
