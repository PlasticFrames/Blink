using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCollision : MonoBehaviour
{
    PlayerMove playerMove;
    PlayerDash dashScript;
    PlayerHealth healthScript;
    EnemySwitch switchScript;
    EnemyTrigger triggerScript;
    EnemyCollision enemyCollision;

    public Rigidbody playerBody;
    public Rigidbody enemyBody;

    public float nudgeForce = 100f;
    public float knockMultiplier = 2f;    
    public float reactionRadius = 2f;

    public Vector3 forceOrigin;

    void Start() 
    {
        playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        healthScript = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        //playerBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();   
    }

    void OnCollisionEnter(Collision other) 
    {
        forceOrigin = other.contacts[0].point;
        Debug.Log(other.contacts[0].otherCollider.tag);
        Debug.Log(other.contacts[0].otherCollider.attachedRigidbody);

        if (other.gameObject.CompareTag("Player") && !dashScript.isDashing)
        {
            //playerBody = other.gameObject.GetComponent<Rigidbody>(); 
            playerBody.AddExplosionForce(nudgeForce * knockMultiplier, forceOrigin, reactionRadius, 0, ForceMode.Impulse);
            StartCoroutine(healthScript.MakeInvulnerable());
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            switchScript = other.gameObject.GetComponent<EnemySwitch>();
            triggerScript = other.gameObject.GetComponent<EnemyTrigger>();
            enemyCollision = other.gameObject.GetComponent<EnemyCollision>();
            enemyBody = other.gameObject.GetComponent<Rigidbody>();

            enemyBody.AddExplosionForce(nudgeForce * knockMultiplier, forceOrigin, reactionRadius, 0, ForceMode.Impulse);

            if(triggerScript.isPushed)
            {
                switch (switchScript.enemyType)
                {
                    case 0:
                        StartCoroutine(enemyCollision.DelayDestroy());
                        break;
                    case 1:
                        switchScript.enemyType = 0;
                        switchScript.CheckType();
                        break;
                    case 2:
                        switchScript.enemyType = 1;
                        switchScript.CheckType();
                        break;
                }   
            }
        }    
    }
}
