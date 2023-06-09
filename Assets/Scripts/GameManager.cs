using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public bool isStarted = false;
    [SerializeField] List<Animator> enemyAnims, trapAnims;
    private void Awake()
    {
        manager = this;
    }
    #region GameEnd
    public void GameEnd()
    {
        isStarted = false;
        StartCoroutine(SceneLoad());
        TrapAnimControl(false);
        PlayerControl.animator.SetBool("Run", false);
        EnemyAnimControl(true, false, 0);
        SaveSystem.save.SetBestScore();
        UIManager.continueScore = true;
        UIManager.restart = true;
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
        UIManager.UI.ScoreAdd(10);
        EnemyAnimControl(false, false, 0);
        Destroy(enemy, .45f);
    }
    #endregion
    #region EnemyAnimControl
    public void EnemyAnimControl(bool enemyState, bool runState, int index)
    {
        if (enemyState)
        {
            for (int i = 0; i < enemyAnims.Count; i++)
            {
                enemyAnims[i].SetBool("Run", runState);
                if (!runState)
                {
                    Destroy(enemyAnims[i]);
                }
            }
            return;
        }
        enemyAnims[index].SetTrigger("Dead");
        enemyAnims.RemoveAt(index);
    }
    #endregion
    #region TrapAnimControl
    public void TrapAnimControl(bool trapState)
    {
        for (int i = 0; i < trapAnims.Count; i++)
        {
            trapAnims[i].SetBool("Rotate", trapState);
            if (!trapState)
            {
                trapAnims[i].gameObject.GetComponent<Rigidbody2D>().simulated = false;
            }
        }
    }
    #endregion
}