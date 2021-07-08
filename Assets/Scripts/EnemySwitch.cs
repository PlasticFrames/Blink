using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySwitch : MonoBehaviour
{  
    public EnemyMovement moveScript;

    public NavMeshAgent agent;

    public int enemyType; //0 = Base, 1 = shield, 2 = armour [ASSIGN IN INSPECTOR]

    public Material heavyDissolveMat;
    public Material mediumDissolveMat;

    public float heavyDissolve;
    public float mediumDissolve;

    void Start()
    {
        moveScript = GetComponent<EnemyMovement>();
        agent = GetComponent<NavMeshAgent>();

        CheckType();
    }
    void Update()
    {
        heavyDissolveMat.SetFloat("heavyDissolve_", heavyDissolve);
        mediumDissolveMat.SetFloat("mediumDissolve_", mediumDissolve);
    }
    
    public void CheckType() //Assigns speed and destroys meshes based on type
    {
        switch (enemyType)
        {
            case 0:
                agent.speed = moveScript.baseSpeed / (enemyType + 1);
                Destroy(gameObject.transform.GetChild(3).gameObject);
                Destroy(gameObject.transform.GetChild(4).gameObject);
                break;
            case 1:
                agent.speed = moveScript.baseSpeed / (enemyType + 1);
                Destroy(gameObject.transform.GetChild(4).gameObject);
                break;
            case 2:
                agent.speed = moveScript.baseSpeed / (enemyType + 1);
                break;
        }
    }
}
