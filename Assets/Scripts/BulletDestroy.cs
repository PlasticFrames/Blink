using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public PlayerDash dashScript;
    public EnemyAttacks attackScript;

    public Rigidbody bulletBody;

    public Vector3 fireVelocity;

    [SerializeField] public float destroyTime;
    public float bulletTimer;

    void Start()
    {
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        attackScript = GameObject.FindWithTag("Enemy").GetComponent<EnemyAttacks>();
        bulletBody = GetComponent<Rigidbody>();
        fireVelocity = bulletBody.velocity;

        bulletTimer = destroyTime;
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
            //Playerhealth -- 
        } 
    }
}
