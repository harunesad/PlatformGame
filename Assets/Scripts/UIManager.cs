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
            GameManager.manager.isStarted = true;
            startPanel.SetActive(false);
            GameManager.manager.EnemyAnimControl(true, true);
            GameManager.manager.TrapAnimControl(true);
        }
        else
        {
            SaveSystem.save.GetBestScore(bestScoreText);
        }
        if (continueScore)
        {
            startPanel.SetActive(false);
            continueScore = false;
            GameManager.manager.isStarted = true;
            GameManager.manager.EnemyAnimControl(true, true);
            GameManager.manager.TrapAnimControl(true);
            SaveSystem.save.GetContinueScore(scoreText);
        }

        startButton.onClick.AddListener(GameStart);
        exitButton.onClick.AddListener(GameExit);
        restartButton.onClick.AddListener(RestartButton);
        lastExitButton.onClick.AddListener(GameExit);
        continueButton.onClick.AddListener(Continue);
    }
    #region DeadPanel
    public void DeadPanel()
    {
        lastScoreText.text = "Score: " + scoreCount;
        SaveSystem.save.GetBestScore(lastBestScoreText);
        restartPanel.SetActive(true);
    }
    #endregion
    #region Restart
    void RestartButton()
    {
        LifeSystem.life.HearthRestart();
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
        GameManager.manager.isStarted = true;
        GameManager.manager.EnemyAnimControl(true, true);
        GameManager.manager.TrapAnimControl(true);
    }
    #endregion
    #region GameExit
    public void GameExit()
    {
        LifeSystem.life.HearthRestart();
        PlayerPrefs.SetFloat(SaveSystem.save.continueScoreKey, 0);
        Application.Quit();
    }
    #endregion
    #region ScoreAdd
    public void ScoreAdd(float inc)
    {
        scoreCount = PlayerPrefs.GetFloat(SaveSystem.save.continueScoreKey);
        scoreCount += inc;
        PlayerPrefs.SetFloat(SaveSystem.save.continueScoreKey, scoreCount);
        scoreText.text = "" + scoreCount.ToString();
    }
    #endregion
}
