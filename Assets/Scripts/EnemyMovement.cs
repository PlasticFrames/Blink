using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public PlayerDash dashScript;

    public Transform target;

    [SerializeField] public float rotationSpeed;

    [SerializeField] public Vector3 yAngle;

    void Start()
    {
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
    }

    void Update()
    {
        if (!dashScript.isDashing && !dashScript.isPlanning)
        {
            if (target != null)
            {
                Vector3 forward = target.position - transform.position;
                forward.y = 0;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forward), rotationSpeed * Time.deltaTime);
            }
            else
            {
                transform.Rotate((yAngle * rotationSpeed) * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.transform;
        }    
    }
    
    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = null;
        }    
    }
}
