using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkPosition : MonoBehaviour
{
    PlayerDash dashScript;

    public GameObject player;

    // Start is called before the first frame update
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
    }

    void AddPosition()
    {
        dashScript.dashMarks.Add(transform.position);
    }
}
