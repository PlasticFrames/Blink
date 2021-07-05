using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTrigger : MonoBehaviour
{
    public EnemyMovement moveScript;
    public EnemyAttacks attackScript;
    public PlayerHealth healthScript;
    public PlayerDash dashScript;

    public GameObject player;
    public NavMeshAgent agent;

    //public int angle = 10;

    public float playerDistance;
    [SerializeField] public float farDistance;
    public float nearDistance;

    public bool isIdle = true;
    public bool isFar = false;
    public bool isNear = false;
    //public bool isFacing = false;

    void Start() 
    {
        moveScript = GetComponent<EnemyMovement>();
        attackScript = GetComponent<EnemyAttacks>();
        healthScript = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        farDistance = GetComponent<SphereCollider>().radius;
        playerDistance = farDistance + 1;
        nearDistance = farDistance / 2; 
    }

    void Update()
    {
        /*if  (Vector3.Angle(player.transform.forward, transform.position - player.transform.position) < angle)
        {
            isFacing = true; //TIE TO FIRING WITHOUT LOOPING ENABLE?
        }*/

        if (healthScript.playerHealth <= 0)
        {
            SetIdle();
        }

        if (!dashScript.isPlanning && !dashScript.isDashing) //Sets booleans according to distance between player and enemy
        {            
            if (playerDistance < farDistance && playerDistance > nearDistance)
            {
                SetFar();
            }
            else if (playerDistance < nearDistance)
            {
                SetNear();
            }
            else
            {
                SetIdle();
            } 
        }
    }

    private void SetNear()
    {
        isIdle = false;
        isNear = true;
        isFar = false;
    }

    private void SetFar()
    {
        isIdle = false;
        isFar = true;
        isNear = false;
    }

    private void SetIdle()
    {
        //agent.enabled = false; //IF NECESSARY
        playerDistance = farDistance + 1;
        isIdle = true;
        isFar = false;
        isNear = false;
    }

    private void OnTriggerStay(Collider other)  //Updates player distance when player is inside trigger
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDistance = Vector3.Distance(transform.position, player.transform.position);
        }     
    }
}
