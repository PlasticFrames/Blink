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

    [SerializeField] float nudgeForce;
    [SerializeField] float nudgeRadius;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Enemy hit");

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
        case 1: playerBody.AddExplosionForce(nudgeForce, transform.position, nudgeRadius); // TIE TO UNIVERSAL FORCE VARIABLE?
        break;
        }
    }    
    
    void DashReaction()
    {
        switch (enemyType)
        {
        case 1: Destroy(gameObject);
        break;
        }
    }
}
