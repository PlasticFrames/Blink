using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash: MonoBehaviour
{
    PlayerMove moveScript;

    public Vector3 playerPos;

    public float dashSpeed;
    public float dashTime;

    public bool isPlanning;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlanning = !isPlanning;
        }

        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }
} 
/*
1. store player position
2. detect mouse click
3. first dash
4. trigger
5. stack second dash
6. stack third dash
7. undo dash
*/ 
