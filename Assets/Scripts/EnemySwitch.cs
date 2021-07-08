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

    public float dissolveTime = 2f;

    void Start()
    {
        moveScript = GetComponent<EnemyMovement>();
        agent = GetComponent<NavMeshAgent>();
        heavyDissolveMat = gameObject.transform.GetChild(4).GetComponent<Renderer>().material;
        mediumDissolveMat = gameObject.transform.GetChild(3).GetComponent<Renderer>().material;
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
                StartCoroutine(DissolveShield(1, dissolveTime));
                agent.speed = moveScript.baseSpeed / (enemyType + 1);
                break;
            case 1:
                StartCoroutine(DissolveArmour(1, dissolveTime));
                agent.speed = moveScript.baseSpeed / (enemyType + 1);
                break;
            case 2:
                agent.speed = moveScript.baseSpeed / (enemyType + 1);
                break;
        }
    }

    IEnumerator DissolveShield(float endValue, float duration)
    {
        float time = 0;
        while(time < duration)
        {
            heavyDissolve = Mathf.Lerp(0, 1, time / duration);
            heavyDissolveMat.SetFloat("heavyDissolve_", heavyDissolve);
            mediumDissolve = Mathf.Lerp(0, 1, time / duration);
            mediumDissolveMat.SetFloat("mediumDissolve_", mediumDissolve);
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject.transform.GetChild(4).gameObject);
        Destroy(gameObject.transform.GetChild(3).gameObject);
    }

    IEnumerator DissolveArmour(float endValue, float duration)
    {
        float time = 0;
        while(time < duration)
        {
            heavyDissolve = Mathf.Lerp(0, 1, time / duration);
            heavyDissolveMat.SetFloat("heavyDissolve_", heavyDissolve);
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject.transform.GetChild(4).gameObject);
    }
}