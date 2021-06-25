using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public PlayerDash dashScript;
    public DashRecharge rechargeScript;

    public GameObject player;
    public GameObject dashRecharge; 
     
    public Rigidbody playerBody;
    public Rigidbody enemyBody;

    float nudgeForce = 10f;
    float knockMultiplier = 2f;    
    float reactionRadius = 2f;

    [SerializeField] public int enemyType; //0 = Base, 1 = shield, 2 = armour

    public Vector3 forceOrigin;
    public Vector3 spawnOffset;

    void Start() 
    {
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        player = GameObject.FindWithTag("Player");
        dashRecharge = GameObject.FindWithTag("Dash Recharge");
        playerBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        enemyBody = GetComponent<Rigidbody>();        
        spawnOffset = new Vector3 (0,2,0);    
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (dashScript.isDashing == false)
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
        case 0: 
            playerBody.AddExplosionForce(nudgeForce, forceOrigin, reactionRadius, 0, ForceMode.Impulse);
            break;
        case 1:
            playerBody.AddExplosionForce(nudgeForce * knockMultiplier, forceOrigin, reactionRadius, 0, ForceMode.Impulse); //ADD PLAYER INPUT DISABLE?
            break;
        case 2:
            playerBody.AddExplosionForce(nudgeForce * knockMultiplier, forceOrigin, reactionRadius, 0, ForceMode.Impulse);
            dashScript.dashCharges--;
            dashScript.maxDash--;
    
            if(dashScript.dashCharges > 0)
            {
                Instantiate(dashRecharge, player.transform.position + spawnOffset, Quaternion.identity);
                rechargeScript = GameObject.FindWithTag("Dash Recharge").GetComponent<DashRecharge>();
                rechargeScript.rechargeDirection = (player.transform.position - transform.position).normalized;
            }    
            break;
        }
    }

    void DashReaction()
    {
        forceOrigin = player.transform.position;

        switch (enemyType)
        {
        case 0: 
            Destroy(gameObject);
            break;
        case 1: 
            enemyBody.AddExplosionForce(nudgeForce * knockMultiplier, forceOrigin, reactionRadius, 0, ForceMode.Impulse);
            break;
        case 2: 
            enemyBody.AddExplosionForce(nudgeForce, forceOrigin, reactionRadius, 0, ForceMode.Impulse);//SWAP TO LERP? RENABLING MOVEMENT MIGHT HELP
            break;
        }
    }
}
