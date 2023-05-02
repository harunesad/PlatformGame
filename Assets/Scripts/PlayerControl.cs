using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    void Update()
    {
        if (!GameManager.manager.isStarted)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnManager.spawnManager.Spawn();
        }
        Application.quitting += UIManager.UI.GameExit;
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
            GameManager.manager.EnemyDead(collision.gameObject.transform.parent.gameObject);
            Rigidbody2D playerRb = FindObjectOfType<PlayerControl>().gameObject.GetComponent<Rigidbody2D>();
            playerRb.velocity = new Vector2(playerRb.position.x, playerRb.position.y + 7);
            return;
        }
        Dead(collision.gameObject);
        if (collision.gameObject.CompareTag("End"))
        {
            UIManager.continueScore = true;
            LifeSystem.life.HearthRestart();
            GameManager.manager.GameEnd();
        }
    }
    #region Dead
    void Dead(GameObject crashObj)
    {
        if (crashObj.layer == 7)
        {
            animator.SetTrigger("Hit");
            GameManager.manager.EnemyAnimControl(true, false);
            GameManager.manager.TrapAnimControl(false);
            sounds[1].Play();
            GameManager.manager.isStarted = false;
            LifeSystem.life.HearthRemove();
            //PlayerPrefs.SetFloat(SaveSystem.save.continueScoreKey, 0);
            UIManager.scoreCount -= UIManager.UI.scoreLevel;
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
        UIManager.restart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
