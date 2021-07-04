using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundCheck : MonoBehaviour
{
    public NavMeshAgent agent;

    public Rigidbody body;

    [SerializeField] public float fallSpeed;

    public bool isGrounded;

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        body = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            if(transform.parent.tag == "Player")
            {
                 
            }
            else if(transform.parent.tag == "Enemy")
            {

            }      
        }    
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

            if (!isGrounded)
            {
                if(transform.parent.tag == "Player")
                {
                    body.drag = 0;
                    body.velocity = Vector3.down * fallSpeed;
                }
                else if(transform.parent.tag == "Enemy")
                {
                    agent.enabled = false;
                    body.drag = 0;
                    body.velocity = Vector3.down * fallSpeed;
                }            
            }      
        }
    }
}
