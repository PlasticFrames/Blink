using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    [SerializeField] public float bulletTimer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, bulletTimer);
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Playerhealth -- 
        } 
    }
}
