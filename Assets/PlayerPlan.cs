using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlan : MonoBehaviour
{
    public TimeManager timeManager;

    public bool isPlanning;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlanning = !isPlanning;

        }

        if (isPlanning)
        {
            timeManager.SlowTime();
        }
    }
}
