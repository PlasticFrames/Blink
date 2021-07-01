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
    [SerializeField] public float ringSize;
    [SerializeField] public float burstSize;
    [SerializeField] public float burstRate;    
    [SerializeField] public float spreadSize;
    [SerializeField] public float spreadRotation;
    
    public float fireTimer = 1f;

    [SerializeField] public Vector3 yAngle;
    [SerializeField] public Vector3 ringRot;
    
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
                SpreadFire();
                break;
            case 2: 
                StartCoroutine(BurstFire());
                break;
        }
    }

    void RingFire()
    {
        for (int i = 0; i < ringSize; i++)
        {
            Instantiate(bulletBody, bulletOffset.transform.position, bulletOffset.transform.rotation);
            bulletOffset.transform.Rotate(ringRot, Space.World);
        }
    }

    IEnumerator BurstFire()
    {
        for (int i = 0; i < burstSize; i++)
        {
            Instantiate(bulletBody, bulletOffset.transform.position, bulletOffset.transform.rotation);
            yield return new WaitForSeconds(burstRate);
        }        
    }

    void SpreadFire()
    {
        bulletOffset.transform.rotation = transform.rotation;
        for (int i = 0; i < spreadSize; i++)
        {
            Instantiate(bulletBody, bulletOffset.transform.position, bulletOffset.transform.rotation);
            bulletOffset.transform.Rotate((yAngle * spreadRotation) * Time.deltaTime);
        }
    }
}