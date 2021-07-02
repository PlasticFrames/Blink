using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public PlayerDash dashScript;

    public GameObject player;
    public Transform target;

    public int State = 0;

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
                State = 1;
            }
            else if (target != null) //enemy is facing player?
            {
                State = 1;
            }
            else
            {
                State = 0;
            }
        }

        switch (State)
        {
            case 0:
                transform.Rotate((yAngle * rotationSpeed) * Time.deltaTime);
                break;
            case 1:
                Vector3 forward = target.position - transform.position;
                forward.y = 0;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forward), rotationSpeed * Time.deltaTime);
                break;
            case 2:

                break;
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
