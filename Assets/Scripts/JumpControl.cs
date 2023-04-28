using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class JumpControl : MonoBehaviour
{
    [SerializeField] LayerMask groundMask, enemyMask;
    public bool isGround = false;
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
        //RaycastHit2D jumpToEnemyHit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, enemyMask);
        Jump(jumpHit);
        //JumpToEnemy(jumpToEnemyHit);
    }
    #region Jump
    void Jump(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            PlayerControl.rb.velocity = new Vector2(PlayerControl.rb.velocity.x, jumpSpeed);
        }
    }
    #endregion
}
