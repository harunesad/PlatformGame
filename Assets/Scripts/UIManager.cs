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
    public static bool continueScore = false;
    [SerializeField] TextMeshProUGUI scoreText, bestScoreText, lastScoreText, lastBestScoreText;
    public float scoreCount;
    [SerializeField] GameObject startPanel, restartPanel;
    [SerializeField] Button startButton, exitButton, restartButton, lastExitButton, continueButton;
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
        if (continueScore)
        {
            startPanel.SetActive(false);
            continueScore = false;
            GameManager.manager.GameState(true);
        }
        SaveSystem.save.SaveText(bestScoreText);
        SaveSystem.save.ContinueText(scoreText);
        //bestScoreText.text = "Best Score: " + PlayerPrefs.GetFloat(bestScoreKey);
        startButton.onClick.AddListener(GameStart);
        exitButton.onClick.AddListener(GameExit);
        restartButton.onClick.AddListener(RestartButton);
        lastExitButton.onClick.AddListener(GameExit);
        continueButton.onClick.AddListener(Continue);
    }
    void Update()
    {
        
    }
    #region DeadPanel
    public void DeadPanel()
    {
        lastScoreText.text = "Score: " + scoreCount;
        SaveSystem.save.HighScoreUpdated();
        SaveSystem.save.SaveText(lastBestScoreText);
        //lastBestScoreText.text = "Best Score: " + PlayerPrefs.GetFloat(bestScoreKey);
        restartPanel.SetActive(true);
    }
    #endregion
    #region Restart
    void RestartButton()
    {
        LifeSystem.life.HearthRestart();
        Restart();
    }
    public void Restart()
    {
        restart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
    #region Continue
    void Continue()
    {
        continueScore = true;
        SaveSystem.save.SceneLoad();
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
        LifeSystem.life.HearthRestart();
        Application.Quit();
    }
    #endregion
    #region ScoreAdd
    public void ScoreAdd(float inc)
    {
        scoreCount = PlayerPrefs.GetFloat("ContinueScore");
        scoreCount += inc;
        PlayerPrefs.SetFloat("ContinueScore", scoreCount);
        scoreText.text = "" + scoreCount.ToString();
    }
    #endregion
}
