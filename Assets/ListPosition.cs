using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListPosition : MonoBehaviour
{
    public PlayerDash dashScript;

    // Start is called before the first frame update
    void Start()
    {
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        AddPosition();
    }

    void AddPosition()
    {
        dashScript.dashMarks.Add(transform.position);
    }
}
