using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpConrol : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] bool isGround = false, doubleJump = false;
    [SerializeField] float jumpSpeed = 4;
    void Start()
    {
        
    }
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, mask);
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
            PlayerControl.rb.velocity = new Vector2(PlayerControl.rb.velocity.x, jumpSpeed);
        }
    }
}
