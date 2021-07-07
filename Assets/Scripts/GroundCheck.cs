using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundCheck : MonoBehaviour
{
    public NavMeshAgent agent;

    public Rigidbody body;

    public float fallSpeed = 20f;

    public bool isGrounded;

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        body = GetComponentInParent<Rigidbody>();
    }
    
    void Update() 
    {
        if (isGrounded)
        {
            body.drag = 5;
        }
        else if (!isGrounded)
        {
            Fall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
        }    
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;     
        }
    }

    private void Fall() //Disables constraints, reduces drag and applies downforce
    {
        if(transform.parent.tag == "Enemy")
        {
            agent.enabled = false;
        }
        body.constraints = ~RigidbodyConstraints.FreezePosition;
        body.drag = 0;
        body.velocity = Vector3.down * fallSpeed;
    }
}
