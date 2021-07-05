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

    public bool isInvulnerable;

    void Start() 
    {
        moveScript = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        playerModel = GameObject.FindWithTag("Player").gameObject.transform.GetChild(2).gameObject;
        playerBody = GetComponent<Rigidbody>();
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

    public IEnumerator MakeInvulnerable()
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
       // playerBody.drag = 0;
        isInvulnerable = false;
    }

    void ScaleModel(Vector3 scale) //CHANGE TO FRENEL OR SOME SUCH?
    {
        playerModel.transform.localScale = scale;
    }

    void ResetPlayer()
    {
        moveScript.enabled = false;
        transform.position = Vector3.zero;
    }
}
