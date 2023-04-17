using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager UI;
    public static bool restart = false;
    [SerializeField] TextMeshProUGUI scoreText, bestScoreText, lastScoreText, lastBestScoreText;
    [SerializeField] float scoreCount;
    [SerializeField] GameObject startPanel, restartPanel;
    [SerializeField] Button startButton, exitButton, restartButton, lastExitButton;
    string bestScoreKey = "HighScore";
    private void Awake()
    {
        UI = GetComponent<UIManager>();
    }
    void Start()
    {
        if (restart)
        {
            GameManager.manager.GameState(true);
            startPanel.SetActive(false);
        }
        bestScoreText.text = "Best Score: " + PlayerPrefs.GetFloat(bestScoreKey);
        startButton.onClick.AddListener(GameStart);
        exitButton.onClick.AddListener(GameExit);
        restartButton.onClick.AddListener(Restart);
        lastExitButton.onClick.AddListener(GameExit);
    }
    void Update()
    {
        
    }
    #region DeadPanel
    public void DeadPanel()
    {
        lastScoreText.text = "Score: " + scoreCount;
        HighScoreUpdated();
        lastBestScoreText.text = "Best Score: " + PlayerPrefs.GetFloat(bestScoreKey);
        restartPanel.SetActive(true);
    }
    #endregion
    #region Restart
    void Restart()
    {
        restart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
    #region HighScoreUpdated
    public void HighScoreUpdated()
    {
        if (scoreCount >= PlayerPrefs.GetFloat(bestScoreKey))
        {
            PlayerPrefs.SetFloat(bestScoreKey, scoreCount);
        }
    }
    #endregion
    #region GameStart
    void GameStart()
    {
        startPanel.SetActive(false);
        GameManager.manager.GameState(true);
    }
    #endregion
    #region GameExit
    void GameExit()
    {
        Application.Quit();
    }
    #endregion
    #region ScoreAdd
    public void ScoreAdd(float inc)
    {
        scoreCount += inc;
        scoreText.text = "" + scoreCount.ToString();
    }
    #endregion
}
