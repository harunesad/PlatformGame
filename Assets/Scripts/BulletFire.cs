using UnityEngine;

public class BulletFire : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8;
    float direction;
    GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        if (collision.gameObject.layer == 7 && collision.gameObject.name == "Enemy")
        {
            Destroy(gameObject);
            GameManager.manager.EnemyDead(collision.gameObject);
            return;
        }

        BulletDestroy(collision.gameObject, 6);
        BulletDestroy(collision.gameObject, 8);
    }
    void BulletDestroy(GameObject obj, int layer)
    {
        if (obj.layer == layer)
        {
            Destroy(gameObject);
        }
    }
}
