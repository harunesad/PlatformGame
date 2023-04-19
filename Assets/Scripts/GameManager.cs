using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public bool isStarted = false;
    [SerializeField] Animator animator;
    private void Awake()
    {
        manager = this;
    }
    void Start()
    {

    }
    void Update()
    {

    }
    #region GameState
    public void GameState(bool state)
    {
        isStarted = state;
    }
    #endregion
    #region GameEnd
    public void GameEnd()
    {
        isStarted = false;
        StartCoroutine(SceneLoad());
        SawControl.animator.SetBool("Rotate", false);
        SawControl.rb.simulated = false;
        PlayerControl.animator.SetBool("Run", false);
        EnemyControl.animator.SetBool("Run", false);
        GameState(false);
    }
    #endregion
    #region SceneLoad
    IEnumerator SceneLoad()
    {
        yield return new WaitForSeconds(1);
        SaveSystem.save.SceneSave();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion
    #region EnemyDead
    public void EnemyDead(GameObject enemy)
    {
        isStarted = false;
        UIManager.UI.ScoreAdd(10);
        SaveSystem.save.HighScoreUpdated();
        EnemyControl.animator.SetTrigger("Dead");
        Destroy(enemy, 1);
    }
    #endregion
}
