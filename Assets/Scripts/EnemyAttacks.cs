using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public PlayerDash dashScript;
    public EnemySwitch switchScript;

    public GameObject player;
    public GameObject bullet;
    public GameObject bulletOffset;

    public Rigidbody bulletBody;
    public Transform target;

    [SerializeField] public float rotationSpeed;
    [SerializeField] public float fireDelay;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float ringRotation;
    [SerializeField] public float burstSize;
    [SerializeField] public float burstDelay;    
    [SerializeField] public float spreadRotation;
    public float fireTimer = 1;

    [SerializeField] public Vector3 yAngle;

    public bool isTriggered = false;

    void Start()
    {
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        switchScript = GetComponent<EnemySwitch>();
        player = GameObject.FindWithTag("Player");
        bulletOffset = (gameObject.transform.GetChild(4).gameObject);
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

        if (isTriggered && !dashScript.isPlanning && !dashScript.isDashing)
        {
            if (fireTimer > 0)
            {
                fireTimer -= Time.deltaTime;
            }
            else
            {
                fireTimer = fireDelay;
            }
        }

        if (fireTimer <= 0)
        {
            FireBullets();
            fireTimer = fireDelay;
        }
    }
    
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.transform;
            fireTimer = fireDelay;
            isTriggered = true;
        }    
    }
    
    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = null;
            isTriggered = false;
        }    
    }

    void FireBullets()
    {
        switch (switchScript.enemyType)
        {
            case 0:
                RingFire(); 
                break;
            case 1:
                StartCoroutine(BurstFire());
                break;
            case 2: 
                SpreadFire();
                break;
        }
    }

    void RingFire()
    {
        bulletOffset.transform.rotation = transform.rotation;
        for (int i = 0; i < 8; i++)
        {
            Instantiate(bulletBody, bulletOffset.transform.position, bulletOffset.transform.rotation);
            bulletOffset.transform.Rotate((yAngle * ringRotation) * Time.deltaTime);
        }
    }

    IEnumerator BurstFire()
    {
        for (int i = 0; i < burstSize; i++)
        {
            Instantiate(bulletBody, bulletOffset.transform.position, bulletOffset.transform.rotation);
            yield return new WaitForSeconds(burstDelay);
        }
        
    }

    void SpreadFire()
    {
        bulletOffset.transform.rotation = transform.rotation;
        for (int i = 0; i < 4; i++)
        {
            Instantiate(bulletBody, bulletOffset.transform.position, bulletOffset.transform.rotation);
            bulletOffset.transform.Rotate((yAngle * spreadRotation) * Time.deltaTime);
        }
    }
}
/*  1.Delay activation ~
    2.Player enters range (set to rotate otherwise?) ~
    3.Enemies fire according to type
    4.Destroy projectile against range OR time ~
    5.Player collision
    6.Player health
*/