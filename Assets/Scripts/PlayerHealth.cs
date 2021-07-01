using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerMove moveScript;

    public int maxHealth = 3;
    public int playerHealth = 3;

    public float invulnerabilityDuration = 1.5f;

    public bool isInvulnerable;

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

    public void TakeDamage()
    {
        if (!isInvulnerable)
        {
            playerHealth--;
            StartCoroutine(MakeInvulnerable());
        }
        else
        {
            return;
        }
    }

    IEnumerator MakeInvulnerable()
    {
        isInvulnerable = true;

        yield return new WaitForSeconds(invulnerabilityDuration);

        isInvulnerable = false;
    }

    void ResetPlayer()
    {
        moveScript.enabled = false;
        transform.position = Vector3.zero;
    }
}
