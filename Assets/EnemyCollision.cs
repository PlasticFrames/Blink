using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] int enemyType; //1 = Base, 2 = shield, 3 = armour

    public PlayerDash dashScript;

    public GameObject player;
    public GameObject rechargeSpawn;
    public GameObject dashRecharge; 
     
    public Rigidbody playerBody;
    public Rigidbody enemyBody;
    public Rigidbody rechargeBody;

    float nudgeForce = 10f;
    float knockMultiplier = 2f;    
    float reactionRadius = 2f;
    float rechargeForce = 1000f;

    public Vector3 forceOrigin;
    public Vector3 rechargeDirection;
    public Vector3 spawnOffset;

    void Start() 
    {
        spawnOffset = new Vector3 (0,2,0);    
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            if  (dashScript.isDashing == false)
            {
                RunReaction();
            }
            else if (dashScript.isDashing == true)
            {
                DashReaction();
            }
        }
    }

    void RunReaction()
    {
        forceOrigin = transform.position;

        switch (enemyType)
        {
        case 1: 
            Debug.Log("Base hit");
            playerBody.AddExplosionForce(nudgeForce, forceOrigin, reactionRadius, 0, ForceMode.Impulse);
            break;
        case 2:
            Debug.Log("Shield hit"); 
            playerBody.AddExplosionForce(nudgeForce * knockMultiplier, forceOrigin, reactionRadius, 0, ForceMode.Impulse); //ADD PLAYER INPUT DISABLE?
            break;
            case 3:
                Debug.Log("Armour hit");
                dashScript.dashCharges--;
                dashScript.maxDash--; //ORB REPRESENTING CHARGES
                SpawnRecharge();
                break;
        }
    }

    void SpawnRecharge()
    {
        rechargeDirection = (player.transform.position - transform.position).normalized;

        if(dashScript.dashCharges > 0)
        {
            Instantiate(dashRecharge, player.transform.position + spawnOffset, Quaternion.identity);
            rechargeBody.AddForce(rechargeDirection * rechargeForce, ForceMode.Impulse);
        }    
    }

    void DashReaction()
    {
        forceOrigin = player.transform.position;

        switch (enemyType)
        {
        case 1: 
            Destroy(gameObject);
            break;
        case 2: 
            enemyBody.AddExplosionForce(nudgeForce * knockMultiplier, forceOrigin, reactionRadius, 0, ForceMode.Impulse);
            break;
        case 3: 
            enemyBody.AddExplosionForce(nudgeForce, forceOrigin, reactionRadius, 0, ForceMode.Impulse);//SWAP TO LERP? RENABLING MOVEMENT MIGHT HELP
            break;
        }
    }
}
