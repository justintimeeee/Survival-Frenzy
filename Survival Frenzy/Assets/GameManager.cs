using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public EnemySpawner spawner;

    [Header("UI")]
    public UIManager ui;

    [Header("Waves")]
    public int wave = 1;
    public int enemiesPerWave = 5;
    public float timeBetweenWaves = 3f;

    [Header("Difficulty Scaling")]
    public float enemySpeedBonusPerWave = 0.25f;
    public int enemyHealthBonusPerWave = 10;

    int score = 0;
    int enemiesAlive = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
    Time.timeScale = 1f;

    // Auto-find EnemySpawner on the same GameObject if Inspector link is missing
    if (spawner == null)
        spawner = GetComponent<EnemySpawner>();

    if (spawner == null)
        Debug.LogError("GameManager: No EnemySpawner component found on this GameObject.");

    UpdateUI();
    StartWave();
    }

    void StartWave()
    {
        enemiesAlive = 0;
        UpdateUI();

        if (spawner != null)
            spawner.SpawnWave(enemiesPerWave);
        else
            Debug.LogError("GameManager: spawner reference is NULL (assign it in inspector).");
    }

    public void RegisterEnemySpawned()
    {
        enemiesAlive++;
        UpdateUI();
    }

    public void RegisterEnemyDied(int points)
    {
        enemiesAlive--;
        score += points;

        if (enemiesAlive <= 0)
        {
            wave++;
            Invoke(nameof(StartWave), timeBetweenWaves);
        }

        UpdateUI();
    }

    public void ApplyWaveScaling(EnemyAI ai, EnemyHealth hp)
    {
        if (ai != null) ai.moveSpeed += (wave - 1) * enemySpeedBonusPerWave;
        if (hp != null) hp.maxHealth += (wave - 1) * enemyHealthBonusPerWave;
    }

    void UpdateUI()
    {
        if (ui == null) return;
        ui.SetScore(score);
        ui.SetWave(wave);
        ui.SetEnemiesLeft(enemiesAlive);
    }
}
