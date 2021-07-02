using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public EnemyMovement moveScript;
    public EnemyAttacks attackScript;

    public float playerDistance;

    public bool isTriggered = false;

    void Start() 
    {
        moveScript = GetComponent<EnemyMovement>();
        attackScript = GetComponent<EnemyAttacks>(); 
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDistance = Vector3.Distance(transform.position, other.transform.position);
        }    
    }
}
