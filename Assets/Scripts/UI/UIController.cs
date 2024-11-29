using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private static UIController instance;
    public static UIController Instance { get { return instance; } }

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI EnemiesText;
    private TextMeshProUGUI timerText;

    private void Awake()
    {
        instance = this;
        scoreText = GetComponent<TextMeshProUGUI>();
        EnemiesText = GetComponent<TextMeshProUGUI>();
        timerText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScore(int value)
    {
        scoreText.text = $"Score: {value}";
    }

    public void UpdateEnemiesCount(int value)
    {
        EnemiesText.text = $"Enemies killed: {value}";
    }

    public void UpdateTimer(float value)
    {
        EnemiesText.text = $"Enemies killed: {value}";
    }
}
