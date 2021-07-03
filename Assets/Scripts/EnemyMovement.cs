using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public EnemySwitch switchScript;
    public EnemyTrigger triggerScript;
    public PlayerDash dashScript;

    public GameObject enemyDestination;
    public GameObject player;
    public NavMeshAgent agent;

    [SerializeField] public float rotationSpeed;
    [SerializeField] public float baseSpeed;
    [SerializeField] public float strafeTime;

    [SerializeField] public Vector3 yAngle;
    public Vector3 retreat;

    public bool strafeToggle;

    void Start()
    {
        switchScript = GetComponent<EnemySwitch>();
        triggerScript = GetComponent<EnemyTrigger>();
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        if (triggerScript.isIdle)
        {
            transform.Rotate((yAngle * rotationSpeed) * Time.deltaTime);
        }
        else if (triggerScript.isFar || triggerScript.isNear) //enemy is facing player?
        {
            Vector3 forward = player.transform.position - transform.position;
            forward.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forward), rotationSpeed * Time.deltaTime);
        }
        
        if (triggerScript.isFar || triggerScript.isNear)
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
            case 0:
                if (triggerScript.isFar)
                {
                    agent.SetDestination(player.transform.position);
                }
                else if (triggerScript.isNear)
                {
                    agent.SetDestination(transform.position - (transform.forward * triggerScript.playerDistance));
                }
                break;
            case 1:
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
            case 2: 
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
