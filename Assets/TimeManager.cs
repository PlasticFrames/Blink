using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    void Update() 
    {
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f); //limits time dilation
    }

    public void SlowTime()
    {
        Time.timeScale = slowdownFactor; //slows time
        Time.fixedDeltaTime = Time.timeScale * 0.02f; //smooths updates
        NormalTime();
    }

    public void NormalTime()
    {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime; //slowly returns to normal
    }
}
