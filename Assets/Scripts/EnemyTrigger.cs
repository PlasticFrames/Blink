using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public EnemyMovement moveScript;
    public EnemyAttacks attackScript;
    public PlayerDash dashScript;

    public GameObject player;

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
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        
        player = GameObject.FindWithTag("Player");

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

        if (!dashScript.isPlanning && !dashScript.isDashing) //Sets booleans according to distance between player and enemy
        {            
            if (playerDistance < farDistance && playerDistance > nearDistance)
            {
                isIdle = false;
                isFar = true;
                isNear = false;
            }
            else if (playerDistance < nearDistance)
            {
                isIdle = false;
                isNear = true;
                isFar = false;
            }
            else
            {
                isIdle = true;
                isFar = false;
                isNear = false;
            } 
        }
        else
        {
            isIdle = false;
            isFar = false;
            isNear = false;
        }
    }

    private void OnTriggerStay(Collider other)  //Updates player distance when player is inside trigger
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDistance = Vector3.Distance(transform.position, player.transform.position);
        }     
    }
}
