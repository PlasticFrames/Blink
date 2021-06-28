using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public EnemySwitch switchScript;

    public GameObject player;

    public Vector3 playerDirection;

    public bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        switchScript = GetComponent<EnemySwitch>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            transform.LookAt(player.transform.position);
            //transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation, Time.time * 0.1f);
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
/*  1.Delay activation
    2.Player enters range (set to rotate otherwise?)
    3.Enemies fire according to type
    4.Destroy projectile against range OR time
    5.Player collision
    6.Player health

    switch (switchScript.enemyType)
        {
        case 0: 
            
            break;
        case 1:
            
            break;
        case 2: 
            
            break;
        }
*/