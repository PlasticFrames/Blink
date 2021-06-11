using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    void Update() 
    {
        Time.timeScale += (1f / slowdownLength) * Time.deltaTime; //returns time to normal
    }

    public void SlowTime()
    {
        Time.timeScale = slowdownFactor; //slows time
        Time.fixedDeltaTime = Time.timeScale * 0.02f; //smooths updates
    }
}
