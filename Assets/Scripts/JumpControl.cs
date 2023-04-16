using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class JumpControl : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] bool isGround = false, doubleJump = false;
    [SerializeField] float jumpSpeed = 4;
    void Start()
    {
        
    }
    void Update()
    {
        if (!GameManager.manager.isStarted)
        {
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, mask);
        Jump(hit);
    }
    #region Jump
    void Jump(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            isGround = true;
            doubleJump = true;
        }
        else
        {
            isGround = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            PlayerControl.rb.velocity = new Vector2(PlayerControl.rb.velocity.x, jumpSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJump)
        {
            doubleJump = false;
            PlayerControl.animator.SetTrigger("DoubleJump");
            PlayerControl.rb.velocity = new Vector2(PlayerControl.rb.velocity.x, jumpSpeed);
        }
    }
    #endregion
}
