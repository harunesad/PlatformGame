using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (collision.gameObject.CompareTag("Enemy") && !GetComponentInChildren<JumpControl>().jumpToEnemy)
        {
            Dead();
        }
        if (collision.gameObject.CompareTag("Fall"))
        {
            Dead();
        }
        if (collision.gameObject.CompareTag("End"))
        {
            GameManager.manager.GameEnd();
        }
    }
    #region Dead
    void Dead()
    {
        animator.SetBool("Run", false);
        EnemyControl.animator.SetBool("Run", false);

        sounds[1].Play();
        GameManager.manager.GameState(false);
        UIManager.UI.DeadPanel();
        Destroy(gameObject, 0.5f);
    }
    #endregion
    #region CoinCollect
    void CoinCollect(GameObject obj, float score)
    {
        Destroy(obj, 0.25f);
        UIManager.UI.ScoreAdd(score);
        sounds[0].Play();
        UIManager.UI.HighScoreUpdated();
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
}
