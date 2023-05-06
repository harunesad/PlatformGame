using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float maxPosX, minPosX;
    float rotate = -1;
    void Update()
    {
        if (!GameManager.manager.isStarted)
        {
            return;
        }
        if (gameObject.transform.position.x < minPosX)
        {
            rotate = 1;
        }
        else if(gameObject.transform.position.x > maxPosX)
        {
            rotate = -1;
        }
        gameObject.transform.localScale = new Vector3(rotate, 1, 1);
        Vector3 pos = new Vector3(rotate * moveSpeed * Time.deltaTime, 0, 0);
        transform.position += pos;
    }
}