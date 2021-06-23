using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRecharge : MonoBehaviour
{
    public PlayerDash dashScript;

    public Rigidbody rechargeBody;

    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnEnable() 
    {

    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ("Player") && rechargeBody.IsSleeping())
        {
            if (dashScript.dashCharges < 3)
            {
                dashScript.dashCharges++;
                Destroy(gameObject);
            }
        }
    }
}
