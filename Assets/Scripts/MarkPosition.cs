using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkPosition : MonoBehaviour
{
    PlayerDash dashScript;

    public GameObject player;

    void Start()
    {
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        player = GameObject.FindWithTag("Player");
        AddPosition();
    }

    void Update() 
    {
        if (player.transform.position == transform.position)
        {
            Destroy(gameObject);            
        }
        else if (!dashScript.isPlanning && !dashScript.isDashing)
        {
            Destroy(gameObject);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            CheckList();
        }
    }

    void AddPosition()
    {
        dashScript.dashMarks.Add(gameObject);
    }

    void CheckList()
    {

    }
}
