using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkPosition : MonoBehaviour
{
    PlayerDash dashScript;

    public GameObject player;

    public int lastIndex;

    public bool lastDestination;

    void Start()
    {
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        player = GameObject.FindWithTag("Player");
        AddMark();
    }

    void Update() 
    {
        if (transform.position == dashScript.dashDestination)
        {
            lastDestination = true;
        }
        else
        {
            lastDestination = false;
        }

        if (player.transform.position == transform.position)
        {
            Destroy(gameObject);            
        }
        else if (!dashScript.isPlanning && !dashScript.isDashing)
        {
            Destroy(gameObject);
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            UndoMark();
        }
    }

    void AddMark()
    {
        dashScript.dashMarks.Add(gameObject);
    }

    public void UndoMark()
    {
        if (lastDestination)
        {
            dashScript.dashMarks.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
