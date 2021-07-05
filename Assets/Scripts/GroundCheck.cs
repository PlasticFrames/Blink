using System;
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
    
    void Update() 
    {
        if(isGrounded)
        {
            body.drag = 5;
        }
        else
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

    private void Fall()
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
