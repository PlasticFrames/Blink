using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] int enemyType; //1 = Base, 2 = shield, 3 = armour

    public PlayerDash dashScript;

    public Collider enemyCollider;
    public GameObject player;  
    public Rigidbody playerBody;
    public Rigidbody enemyBody;

    public float nudgeForce = 10f;
    public float knockMultiplier = 2f;    
    public float reactionRadius = 2f;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag ("Player") && dashScript.isDashing == false)
        {
            RunReaction();
        }
        else if (other.gameObject.CompareTag ("Player") && dashScript.isDashing == true)
        {
            DashReaction();
        }
    }

    void RunReaction()
    {
        switch (enemyType)
        {
        case 1: 
            Debug.Log("Base hit");
            playerBody.AddExplosionForce(nudgeForce, transform.position, reactionRadius, 0, ForceMode.Impulse);
            break;
        case 2:
            Debug.Log("Shield hit"); 
            playerBody.AddExplosionForce(nudgeForce * knockMultiplier, transform.position, reactionRadius, 0, ForceMode.Impulse); //ADD PLAYER INPUT DISABLE?
            break;
        case 3:
            Debug.Log("Armour hit"); 
            dashScript.dashCharges--;
            dashScript.maxDash--; //ORB REPRESENTING CHARGES / TEMPORARILY REDUCE CHARGES
            break;
        }
    }    
    
    void DashReaction()
    {
        switch (enemyType)
        {
        case 1: 
            Destroy(gameObject);
            break;
        case 2: 
            enemyBody.AddExplosionForce(nudgeForce * knockMultiplier, player.transform.position, reactionRadius, 0, ForceMode.Impulse); //SWAP TO LERP? RENABLING MOVEMENT MIGHT HELP
            break;
        case 3: 
            enemyBody.AddExplosionForce(nudgeForce, player.transform.position, reactionRadius, 0, ForceMode.Impulse);
            break;
        }
    }
}
