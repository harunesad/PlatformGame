using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public static Animator animator;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5;
    float rotate = -1;
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
        if (!GameManager.manager.isStarted)
        {
            return;
        }
        animator.SetBool("Run", true);
        if (gameObject.transform.position.x < -4.5f)
        {
            rotate = 1;
        }
        else if(gameObject.transform.position.x > -1.5f)
        {
            rotate = -1;
        }
        gameObject.transform.localScale = new Vector3(rotate, 1, 1);
        rb.velocity = new Vector2(rotate * moveSpeed, rb.velocity.y);
    }
}
