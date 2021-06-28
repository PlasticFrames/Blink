using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public EnemySwitch switchScript;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        switchScript = GetComponent<EnemySwitch>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (switchScript.enemyType)
        {
        case 0: 
            
            break;
        case 1:
            
            break;
        case 2: 
            
            break;
        }   
    }

    void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.LookAt(player.transform);
        }    
    }

}
/*  1.Delay activation
    2.Player enters range OR set time passes
    3.Enemies fire according to type
    4.Destroy projectile against range OR time
    5.Player collision
    6.Player health
*/