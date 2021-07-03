using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public PlayerDash dashScript;

    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    void Start() 
    {
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
    }

    void Update() 
    {
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f); //limits time dilation
        
        /*if (Input.GetKeyDown(KeyCode.Return))
        {
            SlowTime();
        }*/

        if (dashScript.isPlanning)
        {
            StopTime();
        }

        else
        {
            StartTime();
        }
    }
    
    /*public void SlowTime()
    {
        if (!timeStopped)
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f; //smooths updates
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime; //eases return
        }
    }*/

    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void StartTime()
    {
        Time.timeScale = 1f;;
    }
}