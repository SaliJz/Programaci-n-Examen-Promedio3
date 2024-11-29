using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private int score;
    private int enemiesKilled;
    private float timer;
    [SerializeField] private string sceneName;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UIController.Instance.UpdateScore(score);
        UIController.Instance.UpdateEnemiesCount(enemiesKilled);
        UIController.Instance.UpdateTimer(timer);
    }

    public void IncreaseScore()
    {
        score++;
        UIController.Instance.UpdateScore(score);
    }

    public void GameOver()
    {
        GameData.score = score;
        GameData.enemiesKilled = enemiesKilled;
        GameData.timer = timer;
        SceneManager.LoadScene(sceneName);
    }

    public void IncreaseEnemiesKilled()
    {
        enemiesKilled++;
        UIController.Instance.UpdateEnemiesCount(enemiesKilled);
    }

    public void IncreaseTimer()
    {
        timer += Time.deltaTime;
        UIController.Instance.UpdateTimer(timer);
    }
}
