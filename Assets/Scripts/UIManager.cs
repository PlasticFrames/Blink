using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerDash dashScript;

    public GameObject dash0;
    public GameObject dash1;
    public GameObject dash2;
    public GameObject dash3;

    void Start()
    {
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
    }
}
