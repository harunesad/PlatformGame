using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SawControl : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float maxPosY, minPosY;
    [SerializeField] float rotate = -1;
    void Update()
    {
        if (!GameManager.manager.isStarted)
        {
            return;
        }
        if (gameObject.transform.position.y < minPosY)
        {
            rotate = 1;
        }
        else if (gameObject.transform.position.y > maxPosY)
        {
            rotate = -1;
        }
        Vector3 pos = new Vector3(0, rotate * moveSpeed * Time.deltaTime, 0);
        transform.position += pos;
    }
}