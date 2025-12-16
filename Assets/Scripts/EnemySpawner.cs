using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemiesPerWave = 5;
    public float timeBetweenWaves = 5f;
    public float spawnRadius = 10f;

    int enemiesAlive = 0;
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartWave();
    }

    void StartWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector2 circle = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = new Vector3(
                player.position.x + circle.x,
                1f,
                player.position.z + circle.y
            );

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemiesAlive++;

            // Notify spawner when enemy dies
            enemy.GetComponent<EnemyHealth>().onDeath += EnemyDied;
        }
    }

    void EnemyDied()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            // Wait between waves
            Invoke(nameof(StartWave), timeBetweenWaves);
        }
    }
}
