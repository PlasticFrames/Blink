using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public EnemySwitch switchScript;
    public GroundCheck groundScript;
    public EnemyTrigger triggerScript;
    public PlayerDash dashScript;

    public GameObject player;
    public NavMeshAgent agent;

    public float rotationSpeed = 200f;
    public float baseSpeed = 6f;
    public float strafeTime;

    public Vector3 yAngle = new Vector3(0, 0.2f, 0);
    public Vector3 retreat;
    
    public bool recentDash;
    public bool strafeToggle;

    void Start()
    {
        switchScript = GetComponent<EnemySwitch>();
        groundScript = GetComponentInChildren<GroundCheck>();
        triggerScript = GetComponent<EnemyTrigger>();
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        strafeTime = baseSpeed / 2;
    }

    void Update()
    {
        if (triggerScript.isIdle) //Rotates enemy in place
        {
            transform.Rotate((yAngle * rotationSpeed) * Time.deltaTime);
        }
        else if (triggerScript.isFar || triggerScript.isNear) //Rotates enemy towards player
        {
            Vector3 forward = player.transform.position - transform.position;
            forward.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forward), rotationSpeed * Time.deltaTime);
        }
        
        if ((triggerScript.isFar || triggerScript.isNear) && groundScript.isGrounded) //Triggers movement according to distance and type
        {
            agent.enabled = true;
            Move();
        }
        else
        {
            agent.enabled = false;
        }
    }

    void Move()
    {
        switch (switchScript.enemyType)
        {
            case 0: //Base enemy chases or flees
                if (triggerScript.isFar)
                {
                    agent.SetDestination(player.transform.position);
                }
                else if (triggerScript.isNear)
                {
                    agent.SetDestination(transform.position - (transform.forward * triggerScript.playerDistance));
                }
                break;
            case 1: //Shield enemy chases or strafes
                if (triggerScript.isFar)
                {
                    agent.SetDestination(player.transform.position);
                }
                else if (triggerScript.isNear && !strafeToggle)
                {
                    StartCoroutine(MoveLeft());
                }
                else if (triggerScript.isNear && strafeToggle)
                {
                    StartCoroutine(MoveRight());
                }
                break;
            case 2: //Armour enemy chases and knocks
                agent.SetDestination(player.transform.position);                
                break;
        }
    }

    IEnumerator MoveLeft()
    {
        agent.SetDestination(transform.position - (transform.right * triggerScript.playerDistance));
        yield return new WaitForSeconds(strafeTime);
        strafeToggle = true;
    }
    
    IEnumerator MoveRight()
    {
        agent.SetDestination(transform.position + (transform.right * triggerScript.playerDistance));
        yield return new WaitForSeconds(strafeTime);
        strafeToggle = false;
    }
}
