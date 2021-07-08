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
    public Rigidbody enemyBody;

    //public int angle = 10;

    public float playerDistance;
    [SerializeField] public float farDistance;
    public float nearDistance;

    public bool isIdle = true;
    public bool isFar = false;
    public bool isNear = false;
    //public bool isFacing = false;
    public bool isPushed = false;

    void Start() 
    {
        moveScript = GetComponent<EnemyMovement>();
        attackScript = GetComponent<EnemyAttacks>();
        healthScript = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        enemyBody = GetComponent<Rigidbody>();

        farDistance = GetComponent<SphereCollider>().radius;
        playerDistance = farDistance + 1;
        nearDistance = farDistance / 2; 

        GameObject.FindObjectOfType<AudioManager>().Play("Idle");
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

        if (isPushed)
        {
            agent.enabled = false;
            enemyBody.constraints = RigidbodyConstraints.FreezePositionY;
            StartCoroutine(DisablePushed());
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
        playerDistance = farDistance + 1;
        isIdle = true;
        isFar = false;
        isNear = false;

    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player") && !dashScript.isDashing)
        {
            GameObject.FindObjectOfType<AudioManager>().Play("Active");
        }  
    }

    private void OnTriggerStay(Collider other)  //Updates player distance when player is inside trigger
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDistance = Vector3.Distance(transform.position, player.transform.position);
        }     
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("Player") && !dashScript.isDashing)
        {
            GameObject.FindObjectOfType<AudioManager>().Play("Idle");
        }  
    }

    IEnumerator DisablePushed()
    {
        yield return new WaitForSeconds(1f);
        isPushed = false;
        enemyBody.constraints = ~RigidbodyConstraints.FreezePosition;
    }
}
