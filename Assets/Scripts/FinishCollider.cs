using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCollider : MonoBehaviour
{
    GameManager managerScript;

    void Start()
    {
        managerScript = GameObject.FindWithTag("Manager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        managerScript.isComplete = true;
        managerScript.CheckScene();
    }
}
