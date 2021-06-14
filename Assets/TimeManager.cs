using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    public bool timeStopped;

    void Update() 
    {
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f); //limits time dilation

        if (Input.GetKeyDown(KeyCode.Return))
        {
            timeStopped = !timeStopped;
        }

        if (timeStopped)
        {
            StopTime();
        }

        else
        {
            NormTime();
        }
    }

    public void SlowTime()
    {
        if (!timeStopped)
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f; //smooths updates 
        }

    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void NormTime()
    {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime; //slowly returns to normal
    }
}