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
    [SerializeField] Sprite addHearth;
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
            hearth = 6;
        }
        HearthImage();
    }
    #region HearthRemove
    public void HearthRemove()
    {
        hearth--;
        PlayerPrefs.SetFloat(hearthKey, hearth);
    }
    #endregion
    #region HearthUpdate
    public void HearthRestart()
    {
        PlayerPrefs.SetFloat(hearthKey, 6);
        hearth = PlayerPrefs.GetFloat(hearthKey);
    }
    #endregion
    #region HearthUpdate
    public void HearthImage()
    {
        float health = hearth / 2;
        if (health == 0)
        {
            hearths[0].fillAmount = 0;
        }
        for (float i = 0; i < health; i+=.5f)
        {
            hearths[(int)i].fillAmount += .5f;
        }
    }
    #endregion
}