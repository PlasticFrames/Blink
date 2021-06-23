using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeDash : MonoBehaviour
{
    public PlayerDash dashScript;

    public Rigidbody chargeBody;

    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ("Player") && chargeBody.IsSleeping())
        {
            if (dashScript.dashCharges < 3)
            {
                dashScript.dashCharges++;
                Destroy(gameObject);
            }
        }
    }
}
