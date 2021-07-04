using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Rigidbody rigidBody;
    [SerializeField] public float fallSpeed;

    void Start()
    {
        rigidBody = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground");           
        }    
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Air");
            rigidBody.velocity = Vector3.down * fallSpeed;
        }    
    }
}
