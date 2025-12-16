using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public TMP_Text scoreText;
    public TMP_Text waveText;
    public TMP_Text enemiesLeftText;

    public void SetHealth(int current, int max)
    {
        if (healthBar == null || max <= 0) return;
        healthBar.minValue = 0f;
        healthBar.maxValue = 1f;
        healthBar.value = (float)current/max;
    }

    public void SetScore(int score)
    {
        if (scoreText != null) scoreText.text = $"Score: {score}";
    }

    public void SetWave(int wave)
    {
        if (waveText != null) waveText.text = $"Wave {wave}";
    }

    public void SetEnemiesLeft(int left)
    {
        if (enemiesLeftText != null) enemiesLeftText.text = $"Enemies: {left}";
    }
}
