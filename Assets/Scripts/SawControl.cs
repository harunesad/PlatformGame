using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SawControl : MonoBehaviour
{
    public static Animator animator;
    public static Rigidbody2D rb;
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
        animator.SetBool("Rotate", true);
        if (gameObject.transform.position.y < 1)
        {
            rotate = 1;
        }
        else if (gameObject.transform.position.y > 3)
        {
            rotate = -1;
        }
        rb.velocity = new Vector2(rb.velocity.x, moveSpeed * rotate);
    }
}
