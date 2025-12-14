using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int pointsOnDeath = 10;

    public Action onDeath;

    int currentHealth;
    bool dead;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (dead) return;

        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (dead) return;
        dead = true;

        onDeath?.Invoke();

        if (GameManager.Instance != null)
            GameManager.Instance.RegisterEnemyDied(pointsOnDeath);

        Destroy(gameObject);
    }
}
