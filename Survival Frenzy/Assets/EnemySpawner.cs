using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 10f;

    Transform player;

    void Start()
    {
        TryFindPlayer();

        if (enemyPrefab == null)
            Debug.LogError("EnemySpawner: enemyPrefab is NOT assigned in the Inspector.");
    }

    void Update()
    {
        // If player wasn't found yet (or got replaced), keep trying.
        if (player == null)
            TryFindPlayer();
    }

    void TryFindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            // Debug.Log("EnemySpawner: Player found.");
        }
        else
        {
            // Leave this as a warning so it doesn't spam your console as hard as Error.
            Debug.LogWarning("EnemySpawner: No GameObject tagged 'Player' found yet.");
        }
    }

    public void SpawnWave(int count)
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("EnemySpawner: SpawnWave called but enemyPrefab is null.");
            return;
        }

        if (player == null)
            TryFindPlayer();

        if (player == null)
        {
            Debug.LogError("EnemySpawner: SpawnWave called but player is still null. Make sure your Player object is tagged 'Player'.");
            return;
        }

        Debug.Log($"EnemySpawner: Spawning {count} enemies.");

        for (int i = 0; i < count; i++)
        {
            Vector2 circle = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = new Vector3(
                player.position.x + circle.x,
                1f,
                player.position.z + circle.y
            );

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            var ai = enemy.GetComponent<EnemyAI>();
            var hp = enemy.GetComponent<EnemyHealth>();

            if (GameManager.Instance != null)
            {
                GameManager.Instance.ApplyWaveScaling(ai, hp);
                GameManager.Instance.RegisterEnemySpawned();
            }
            else
            {
                Debug.LogWarning("EnemySpawner: GameManager.Instance is null.");
            }
        }
    }
}
