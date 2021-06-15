using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash: MonoBehaviour
{
    PlayerMove moveScript;

    public Camera cam;

    public GameObject mouseGhost;
    
    public float groundZ = 0f;
    public float distance;
    public float dashSpeed;
    public float dashTime;

    public Vector3 playerPosition;
    public Vector3 mousePosition;
    public Vector3 dashDestination;

    public bool isPlanning;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = GetWorldPosition(groundZ);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlanning = !isPlanning;
            
            playerPosition = transform.position;
        }

        if (isPlanning)
        {
            mouseGhost.SetActive(true);
            Plan();
        }
        else
        {
            mouseGhost.SetActive(false);
        }

        if(Input.GetMouseButtonDown(0) && isPlanning)
        {
            //StartCoroutine(Dash());
        }
    }

    void Plan()
    {
        mouseGhost.transform.position = mousePosition;
        mouseGhost.transform.rotation = transform.rotation;
    }

    public Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0, z, 0));
        ground.Raycast(mousePos, out distance);
        //mousePosition = mousePos;
        return mousePos.GetPoint(distance);
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
1. store player position /
2. detect mouse click
3. first dash
4. trigger
5. stack second dash
6. stack third dash
7. undo dash
*/ 
