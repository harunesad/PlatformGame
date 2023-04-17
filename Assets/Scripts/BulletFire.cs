using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8;
    float direction;
    GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    void Start()
    {
        direction = player.transform.localScale.x;
    }
    void Update()
    {
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            GameManager.manager.EnemyDead(collision.gameObject);
        }
    }

}
