using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    public static LifeSystem life;
    public float hearth;
    string hearthKey = "Hearth";
    [SerializeField] List<Image> hearths;
    [SerializeField] Sprite addHearth, removeHearth;
    private void Awake()
    {
        life = this;
    }
    void Start()
    {
        if (PlayerPrefs.HasKey(hearthKey))
        {
            hearth = PlayerPrefs.GetFloat(hearthKey);
        }
        else
        {
            hearth = 3;
        }
        //if (hearth == 0)
        //{
        //    HearthRestart();
        //}

        HearthImage();
    }
    void Update()
    {
        
    }
    #region HearthRemove
    public void HearthRemove()
    {
        hearth--;
        PlayerPrefs.SetFloat(hearthKey, hearth);
    }
    #region HearthUpdate
    public void HearthRestart()
    {
        PlayerPrefs.SetFloat(hearthKey, 3);
        hearth = PlayerPrefs.GetFloat(hearthKey);
    }
    #endregion
    #endregion
    #region HearthUpdate
    public void HearthImage()
    {
        for (int i = 0; i < hearth; i++)
        {
            hearths[i].sprite = addHearth;
        }
        for (int i = (int)hearth; i < 3; i++)
        {
            hearths[i].sprite = removeHearth;
        }
    }
    #endregion
}
