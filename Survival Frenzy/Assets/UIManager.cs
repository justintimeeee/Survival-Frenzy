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
        if (healthBar == null) return;
        healthBar.maxValue = max;
        healthBar.value = current;
    }

    public void SetScore(int score)
    {
        if (scoreText != null) scoreText.text = $"Score: {score}";
    }

    public void SetWave(int wave)
    {
        if (waveText != null) waveText.text = $"Wave: {wave}";
    }

    public void SetEnemiesLeft(int left)
    {
        if (enemiesLeftText != null) enemiesLeftText.text = $"Enemies Left: {left}";
    }
}
