using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerMove moveScript;

    public GameObject playerModel;

    public Rigidbody playerBody;

    public int maxHealth = 3;
    public int playerHealth = 3;

    public float invulnerabilityDuration = 1.5f;
    public float invulnerabilityDelta = 0.15f;

    public Vector3 startPosition;

    public bool isInvulnerable;

    void Start() 
    {
        moveScript = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        playerModel = GameObject.FindWithTag("Player").gameObject.transform.GetChild(0).gameObject;
        playerBody = GetComponent<Rigidbody>();
        
        startPosition = transform.position;
    }

    void Update() 
    {
        if (playerHealth <= 0)
        {
            ResetPlayer();
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

    public IEnumerator MakeInvulnerable() //Scales player model for set time to simulate flashing
    {
        isInvulnerable = true;

        for (float i = 0; i < invulnerabilityDuration; i += invulnerabilityDelta)
        {
            if (playerModel.transform.localScale == Vector3.one)
            {
                ScaleModel(Vector3.zero);
            }
            else
            {
                ScaleModel(Vector3.one);
            }
            yield return new WaitForSeconds(invulnerabilityDelta);
        }
        ScaleModel(Vector3.one);
        isInvulnerable = false;
    }

    void ScaleModel(Vector3 scale)
    {
        playerModel.transform.localScale = scale;
    }

    void ResetPlayer()
    {
        moveScript.enabled = false;
        transform.position = startPosition;
        playerHealth = maxHealth;
        moveScript.enabled = true;
    }
}
