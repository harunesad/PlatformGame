using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class JumpControl : MonoBehaviour
{
    [SerializeField] LayerMask groundMask, enemyMask;
    public bool isGround = false, doubleJump = false, jumpToEnemy = false;
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
        RaycastHit2D jumpHit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, groundMask);
        RaycastHit2D jumpToEnemyhit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, enemyMask);
        Jump(jumpHit);
        JumpToEnemy(jumpToEnemyhit);
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
    #region JumpToEnemy
    void JumpToEnemy(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            jumpToEnemy = true;
            GameManager.manager.EnemyDead(hit.collider.gameObject);
        }
        else
        {
            jumpToEnemy = false;
        }
    }
    #endregion
}
