using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public bool isStarted = false;
    private void Awake()
    {
        manager = this;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    #region GameState
    public void GameState(bool state)
    {
        isStarted = state;
    }
    #endregion
    //#region GameStop
    //public void GameStop()
    //{
    //    if (!isStarted)
    //    {
    //        Debug.Log("adssad");
    //        PlayerControl.animator.SetBool("Run", false);
    //        return;
    //    }
    //}
    //#endregion
}
