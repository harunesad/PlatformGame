using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem save;
    public string bestScoreKey = "HighScore", sceneKey = "Scene";

    private void Awake()
    {
        save = this;
    }
    #region SaveText
    public void GetBestScore(TextMeshProUGUI scoreText)
    {
        scoreText.text = "Best Score: " + PlayerPrefs.GetFloat(bestScoreKey);
    }
    #endregion
    #region HighScoreUpdated
    public void SetBestScore()
    {
        if (UIManager.scoreCount >= PlayerPrefs.GetFloat(bestScoreKey))
        {
            PlayerPrefs.SetFloat(bestScoreKey, UIManager.scoreCount);
        }
    }
    #endregion
    #region SceneSave
    public void SceneSave()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 > PlayerPrefs.GetInt(sceneKey))
        {
            PlayerPrefs.SetInt(sceneKey, SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    #endregion
    #region SceneLoad
    public void SceneLoad()
    {
        if (PlayerPrefs.GetInt(sceneKey) > 0)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt(sceneKey));
        }
    }
    #endregion
}
