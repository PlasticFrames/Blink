using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public PlayerDash dashScript;
    public PlayerHealth healthScript;
    public EnemyAttacks attackScript;

    public Rigidbody bulletBody;

    public Vector3 fireVelocity;

    [SerializeField] public float destroyTime;
    public float bulletTimer;
    [SerializeField] public float bulletSpeed;

    void Start()
    {
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        healthScript = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        attackScript = GameObject.FindWithTag("Enemy").GetComponent<EnemyAttacks>();
        bulletBody = GetComponent<Rigidbody>();

        bulletTimer = destroyTime;
        bulletBody.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
        if (!dashScript.isDashing && !dashScript.isPlanning)
        {
            //bulletBody.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
        }
        fireVelocity = bulletBody.velocity;
    }

    void Update() 
    {
        if (!dashScript.isDashing && !dashScript.isPlanning)
        {
            bulletTimer -= Time.deltaTime;
        }

        if (bulletTimer <= 0)
        {
            Destroy(gameObject);
        }
        
        if (dashScript.isDashing || dashScript.isPlanning)
        {
            bulletBody.constraints =  RigidbodyConstraints.FreezeAll;
        }
        else if (!dashScript.isDashing && !dashScript.isPlanning)
        {
            bulletBody.constraints = RigidbodyConstraints.None;
            bulletBody.velocity = fireVelocity;
        } 
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!dashScript.isDashing && !dashScript.isPlanning)
            {
                healthScript.TakeDamage();
                Destroy(gameObject);
            }
        } 
    }
}
