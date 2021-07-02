using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public EnemySwitch switchScript;
    public EnemyTrigger triggerScript;
    public PlayerDash dashScript;

    public GameObject player;
    public NavMeshAgent agent;

    [SerializeField] public float rotationSpeed;

    [SerializeField] public Vector3 yAngle;

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
        else if (triggerScript.isMoving || triggerScript.isAttacking) //enemy is facing player?
        {
            Vector3 forward = player.transform.position - transform.position;
            forward.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forward), rotationSpeed * Time.deltaTime);
        }
        
        if (triggerScript.isMoving)
        {
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
                agent.enabled = true;
                agent.SetDestination(player.transform.position);
                break;
            case 1:
                agent.enabled = true;
                agent.SetDestination(player.transform.position);               
                break;
            case 2: 
                agent.enabled = true;
                agent.SetDestination(player.transform.position);                
                break;
        }
    }
}
