using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerHealth healthScript;
    public PlayerDash dashScript;

    public GameObject dash0;
    public GameObject dash1;
    public GameObject dash2;
    public GameObject dash3;
    public GameObject health0;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;

    void Start()
    {
        healthScript = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
    }

    void Update() //BRUTE FORCE SOLUTION WITH LOTS OF ISSUES
    {
        switch(dashScript.dashCharges)
        {
        case 0:
            dash0.SetActive (true);
            dash1.SetActive (false);
            dash2.SetActive (false);
            dash3.SetActive (false);
            break;
        case 1:
            dash0.SetActive (false);
            dash1.SetActive (true);
            dash2.SetActive (false);
            dash3.SetActive (false);
            break;
        case 2:
            dash0.SetActive (false);
            dash1.SetActive (false);
            dash2.SetActive (true);
            dash3.SetActive (false);
            break;
        case 3:
            dash0.SetActive (false);
            dash1.SetActive (false);
            dash2.SetActive (false);
            dash3.SetActive (true);
            break;
        }

        switch(healthScript.playerHealth)
        {
        case 0:
            health0.SetActive (true);
            health1.SetActive (false);
            health2.SetActive (false);
            health3.SetActive (false);
            break;
        case 1:
            health0.SetActive (false);
            health1.SetActive (true);
            health2.SetActive (false);
            health3.SetActive (false);
            break;
        case 2:
            health0.SetActive (false);
            health1.SetActive (false);
            health2.SetActive (true);
            health3.SetActive (false);
            break;
        case 3:
            health0.SetActive (false);
            health1.SetActive (false);
            health2.SetActive (false);
            health3.SetActive (true);
            break;
        }        
    }
}
