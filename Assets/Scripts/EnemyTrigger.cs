using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public EnemyMovement moveScript;
    public EnemyAttacks attackScript;
    public PlayerDash dashScript;

    public float playerDistance;
    [SerializeField] public float moveDistance;
    public float attackDistance;

    public bool isIdle = true;
    public bool isMoving = false;
    public bool isAttacking = false;

    void Start() 
    {
        moveScript = GetComponent<EnemyMovement>();
        attackScript = GetComponent<EnemyAttacks>();
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        
        playerDistance = 100f;
        moveDistance = GetComponent<SphereCollider>().radius;
        attackDistance = moveDistance / 2; 
    }

    void Update()
    {
        if (!dashScript.isPlanning && !dashScript.isDashing)
        {
            if (playerDistance < moveDistance && playerDistance > attackDistance)
            {
                isIdle = false;
                isMoving = true;
                isAttacking = false;
            }
            else if (playerDistance < attackDistance)
            {
                isAttacking = true;
                isMoving = false;
            }
            else
            {
                isIdle = true;
                isMoving = false;
                isAttacking = false;
            } 
        }
        else
        {
            isIdle = false;
            isMoving = false;
            isAttacking = false;
        }
    }

    void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDistance = Vector3.Distance(transform.position, other.transform.position);
        }   
    }
}
