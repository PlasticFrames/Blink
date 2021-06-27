using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwitch : MonoBehaviour
{  
    public int enemyType; //0 = Base, 1 = shield, 2 = armour

    void Start()
    {
        CheckType();
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
           CheckType(); 
        }
    }

    void CheckType()
    {
        switch (enemyType)
        {
            case 0:
                Destroy(gameObject.transform.GetChild(1).gameObject);
                Destroy(gameObject.transform.GetChild(2).gameObject);
                break;
            case 1:
                Destroy(gameObject.transform.GetChild(2).gameObject);
                break;
        }
    }
}
