using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static Rigidbody2D rb;
    public static Animator animator;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] AudioSource coinSound, deathSound;
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
        float h = Input.GetAxis("Horizontal");
        MoveAndRotate(h);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Apple"))
        {
            CoinCollect(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fall"))
        {
            Dead();
        }
    }
    #region Dead
    void Dead()
    {
        deathSound.Play();
        GameManager.manager.GameState(false);
        UIManager.UI.DeadPanel();
        Destroy(gameObject, 0.5f);
    }
    #endregion
    #region CoinCollect
    void CoinCollect(GameObject obj)
    {
        Destroy(obj, 0.25f);
        UIManager.UI.ScoreAdd(5);
        coinSound.Play();
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
