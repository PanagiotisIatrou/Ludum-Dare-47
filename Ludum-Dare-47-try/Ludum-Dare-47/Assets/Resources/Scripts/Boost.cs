using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public static bool boost;
    private float time;
    private float boosttime;
    private bool flage;
    public AudioClip boostclip;
    private void Start()
    {
        boost = false;
        flage = true;
        time = 11;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)&& time >2 && flage)
        {
            AudioSource.PlayClipAtPoint(boostclip, Vector3.zero, 1f);
            time = 0;
            boost = true;
            boosttime = 0;
            flage = false;
        }
        if (!flage)
        {
            boosttime += Time.deltaTime;
        }
        if (boosttime > 1)
        {
            boost = false;
            flage = true;
        }
    }

    public static bool BOOST()
    {
        return boost;
    }
}
