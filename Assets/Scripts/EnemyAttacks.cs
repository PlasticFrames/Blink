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
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float fireDelay;
    [SerializeField] public float fireSpeed;

    public float fireTimer;

    [SerializeField] public Vector3 yAngle;

    public bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        switchScript = GetComponent<EnemySwitch>();
        player = GameObject.FindWithTag("Player");
        bulletOffset = (gameObject.transform.GetChild(4).gameObject);
    }

    // Update is called once per frame
    void Update()
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
        Debug.Log(fireTimer);

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
                FrontFire();
                BackFire();
                LeftFire();
                RightFire();
                break;
            case 1:
            
            break;
        case 2: 
            
            break;
        }
    }

    private void FrontFire()
    {
        Rigidbody clone = Instantiate(bulletBody, bulletOffset.transform.position, bulletOffset.transform.rotation);
        clone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
    }

    private void BackFire()
    {
        Rigidbody clone = Instantiate(bulletBody, bulletOffset.transform.position, bulletOffset.transform.rotation);
        clone.velocity = transform.TransformDirection(Vector3.back * bulletSpeed);
    }

    private void LeftFire()
    {
        Rigidbody clone = Instantiate(bulletBody, bulletOffset.transform.position, bulletOffset.transform.rotation);
        clone.velocity = transform.TransformDirection(Vector3.left * bulletSpeed);
    }

    private void RightFire()
    {
        Rigidbody clone = Instantiate(bulletBody, bulletOffset.transform.position, bulletOffset.transform.rotation);
        clone.velocity = transform.TransformDirection(Vector3.right * bulletSpeed);
    }
}
/*  1.Delay activation ~
    2.Player enters range (set to rotate otherwise?) ~
    3.Enemies fire according to type
    4.Destroy projectile against range OR time ~
    5.Player collision
    6.Player health


*/