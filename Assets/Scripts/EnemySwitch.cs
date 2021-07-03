using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySwitch : MonoBehaviour
{  
    public EnemyMovement moveScript;

    public NavMeshAgent agent;

    public int enemyType; //0 = Base, 1 = shield, 2 = armour

    void Start()
    {
        moveScript = GetComponent<EnemyMovement>();
        agent = GetComponent<NavMeshAgent>();
        
        CheckType();
    }

    public void CheckType()
    {
        switch (enemyType)
        {
            case 0:
                agent.speed = moveScript.baseSpeed / (enemyType + 1);
                Destroy(gameObject.transform.GetChild(1).gameObject);
                Destroy(gameObject.transform.GetChild(2).gameObject);
                break;
            case 1:
                agent.speed = moveScript.baseSpeed / (enemyType + 1);
                Destroy(gameObject.transform.GetChild(2).gameObject);
                break;
            case 2:
                agent.speed = moveScript.baseSpeed / (enemyType + 1);
                break;
        }
    }
}
