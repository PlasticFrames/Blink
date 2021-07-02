using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public EnemyMovement moveScript;
    public EnemyAttacks attackScript;
    public PlayerDash dashScript;

    public GameObject player;

    public int angle = 10;

    public float playerDistance;
    [SerializeField] public float moveDistance;
    public float attackDistance;

    public bool isIdle = true;
    public bool isMoving = false;
    public bool isFacing = false;
    public bool isAttacking = false;
    public bool isCharging = false; //Charge > fire when within certain range

    void Start() 
    {
        moveScript = GetComponent<EnemyMovement>();
        attackScript = GetComponent<EnemyAttacks>();
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        
        player = GameObject.FindWithTag("Player");

        moveDistance = GetComponent<SphereCollider>().radius;
        playerDistance = moveDistance + 1;
        attackDistance = moveDistance / 2; 
    }

    void Update()
    {
        if  (Vector3.Angle(player.transform.forward, transform.position - player.transform.position) < angle)
        {
            isFacing = true; //TIE TO FIRING WITHOUT LOOPING ENABLE?
        }

        if (!dashScript.isPlanning && !dashScript.isDashing)
        {
            if (playerDistance < moveDistance && playerDistance > attackDistance)
            {
                isIdle = false;
                isMoving = true;
                isAttacking = true;
            }
            else if (playerDistance < attackDistance)
            {
                isAttacking = false;
                isMoving = true;
            }
            else
            {
                isIdle = true;
                isMoving = false;
                isFacing = false;
                isAttacking = false;
            } 
        }
        else
        {
            isIdle = false;
            isMoving = false;
            isFacing = false;
            isAttacking = false;
        }
    }

    void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDistance = Vector3.Distance(transform.position, player.transform.position);
        }   
    }
}
