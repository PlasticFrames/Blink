using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables : MonoBehaviour
{
    public PlayerMove moveScript;

    public int maxHealth = 3;
    public int playerHealth = 3;

    void Start() 
    {
        moveScript = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
    }

    void Update() 
    {
        if (playerHealth <= 0)
        {
            ResetPlayer();
            moveScript.enabled = false;
            playerHealth = maxHealth;
        }
    }

    void ResetPlayer()
    {
        moveScript.enabled = false;
        transform.position = Vector3.zero;
    }
}
