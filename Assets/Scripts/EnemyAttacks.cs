using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public EnemySwitch switchScript;

    public GameObject player;
    public GameObject bullet;
    public GameObject bulletOffset;

    public Rigidbody bulletBody;

    [SerializeField] public Vector3 yAngle;

    public bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        switchScript = GetComponent<EnemySwitch>();
        player = GameObject.FindWithTag("Player");
        bulletOffset = (gameObject.transform.GetChild(4).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTriggered)
        {
            transform.Rotate(yAngle, Space.Self);
        }
        
        if (isTriggered)
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
            Rigidbody clone;
            clone = Instantiate(bulletBody, bulletOffset.transform.position, transform.rotation);
            clone.velocity = transform.TransformDirection(Vector3.forward * 10);
            break;
        case 1:
            
            break;
        case 2: 
            
            break;
        }
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