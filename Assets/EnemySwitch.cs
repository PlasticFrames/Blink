using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwitch : MonoBehaviour
{
    public EnemyCollision collisionScript;
    
    public int enemyType; //0 = Base, 1 = shield, 2 = armour

    void Start() 
    {
        switch (enemyType)
        {
            case 0:
                Destroy(transform.GetChild (1));
                break;
        }    
    }
}
