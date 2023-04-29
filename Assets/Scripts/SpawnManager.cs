using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager spawnManager;
    public static GameObject bullet;
    [SerializeField] GameObject spawnBullet;
    [SerializeField] GameObject bulletPoint;
    private void Awake()
    {
        spawnManager = this;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    #region Spawn
    public void Spawn()
    {
        bullet = Instantiate(spawnBullet, bulletPoint.transform.position, Quaternion.identity);
    }
    #endregion
}
