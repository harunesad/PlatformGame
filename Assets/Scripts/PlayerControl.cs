using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerControl : MonoBehaviour
{
    public static Rigidbody2D rb;
    public static Animator animator;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] AudioSource[] sounds;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        //GameManager.manager.GameStop();
        if (!GameManager.manager.isStarted)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnManager.spawnManager.Spawn();
        }
        float h = Input.GetAxis("Horizontal");
        MoveAndRotate(h);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            CoinCollect(collision.gameObject, 4);
        }
        if (collision.gameObject.CompareTag("SuperCoin"))
        {
            CoinCollect(collision.gameObject, 8);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Head"))
        {
            GameManager.manager.EnemyDead(collision.gameObject);
            Debug.Log(collision.gameObject.name);
            return;
        }
        Dead(collision.gameObject, "Enemy");
        Dead(collision.gameObject, "Fall");
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    Dead();
        //}
        //if (collision.gameObject.CompareTag("Fall"))
        //{
        //    Dead();
        //}
        if (collision.gameObject.CompareTag("End"))
        {
            UIManager.continueScore = true;
            LifeSystem.life.HearthRestart();
            SaveSystem.save.ContinueScore(UIManager.UI.scoreCount);
            GameManager.manager.GameEnd();
        }
    }
    #region Dead
    void Dead(GameObject crashObj, string tag)
    {
        if (crashObj.CompareTag(tag))
        {
            Debug.Log(crashObj.name);
            animator.SetTrigger("Hit");
            if (FindObjectOfType<EnemyControl>() != null)
            {
                EnemyControl.animator.SetBool("RunLeft", false);
            }
            SawControl.animator.SetBool("Rotate", false);
            SawControl.rb.simulated = false;

            sounds[1].Play();
            GameManager.manager.GameState(false);

            LifeSystem.life.HearthRemove();
            PlayerPrefs.SetFloat(SaveSystem.save.continueScoreKey, 0);
            if (LifeSystem.life.hearth == 0)
            {
                UIManager.continueScore = false;
                LifeSystem.life.HearthImage();
                UIManager.UI.DeadPanel();
            }
            else
            {
                StartCoroutine(SceneLoad());
            }
            Destroy(gameObject, 0.5f);
        }
    }
    #endregion
    #region CoinCollect
    void CoinCollect(GameObject obj, float score)
    {
        Destroy(obj, 0.25f);
        UIManager.UI.ScoreAdd(score);
        sounds[0].Play();
        SaveSystem.save.HighScoreUpdated();
    }
    #endregion
    #region MoveAndRotate
    void MoveAndRotate(float horizontal)
    {
        bool runState;
        if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            runState = true;
        }
        else if (horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            runState = true;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            runState = false;
        }
        animator.SetBool("Run", runState);
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }
    #endregion
    IEnumerator SceneLoad()
    {
        yield return new WaitForSeconds(0.4f);
        UIManager.continueScore = true;
        UIManager.UI.Restart();
    }
}
