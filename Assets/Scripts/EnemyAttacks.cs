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

    [SerializeField] public float bulletSpeed;

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
        if (!isTriggered && !dashScript.isDashing && !dashScript.isPlanning)
        {
            transform.Rotate(yAngle, Space.Self);
        }
        
        if (isTriggered && !dashScript.isDashing && !dashScript.isPlanning)
        {
            transform.LookAt(player.transform.position);
            
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            FireBullets();
        }
    }

    void FireBullets()
    {
        switch (switchScript.enemyType)
        {
            case 0:
                //FrontFire();
                //BackFire();
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
        Rigidbody clone;
        clone = Instantiate(bulletBody, bulletOffset.transform.position, bulletOffset.transform.rotation);
        clone.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
    }

    private void BackFire()
    {
        Rigidbody clone;
        clone = Instantiate(bulletBody, bulletOffset.transform.position, bulletOffset.transform.rotation);
        clone.velocity = transform.TransformDirection(Vector3.back * bulletSpeed);
    }

    private void LeftFire()
    {
        Rigidbody clone;
        clone = Instantiate(bulletBody, bulletOffset.transform.position, bulletOffset.transform.rotation);
        clone.velocity = transform.TransformDirection(Vector3.left * bulletSpeed);
    }

    private void RightFire()
    {
        Rigidbody clone;
        clone = Instantiate(bulletBody, bulletOffset.transform.position, bulletOffset.transform.rotation);
        clone.velocity = transform.TransformDirection(Vector3.right * bulletSpeed);
    }
    
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
        }    
    }
    
    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggered = false;
        }    
    }
}
/*  1.Delay activation ~
    2.Player enters range (set to rotate otherwise?) ~
    3.Enemies fire according to type
    4.Destroy projectile against range OR time
    5.Player collision
    6.Player health


*/