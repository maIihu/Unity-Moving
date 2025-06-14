using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGameManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;

    public static LevelGameManager Instance;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }
    
    private void Start()
    {
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
    }

    private void Update()
    {
        
    }

    public void ButtonPause()
    {
        Debug.Log("Button pause");
        GameManager.Instance.ChangeState(GameState.Paused);
        pausePanel.SetActive(true);
    }

    public void ButtonPlay()
    {
        pausePanel.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Playing);
    }

    public void ButtonRepeatLevel()
    {
        gameOverPanel.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Playing);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ButtonBackLevelsMenu()
    {
        GameManager.Instance.ChangeState(GameState.Menu);
        SceneManager.LoadScene("MainMenu");
    }

    public void CompleteLevel()
    {
        levelCompletePanel.SetActive(true);
        GameManager.Instance.ChangeState(GameState.GameOver);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
