using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] float moveSpeed = 10;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        MoveAndRotate(h);
    }
    void MoveAndRotate(float horizontal)
    {
        bool runState = false;
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
}
