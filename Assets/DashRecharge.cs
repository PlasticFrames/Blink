using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRecharge : MonoBehaviour
{
    public PlayerDash dashScript;

    public Rigidbody rechargeBody;

    public Vector3 rechargeDirection;

    [SerializeField] float rechargeForce;

    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnEnable() 
    {
        rechargeBody.AddForce(rechargeDirection * rechargeForce, ForceMode.Impulse);
        Cooldown();
    }

    private void Cooldown()
    {
        //();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            dashScript.dashCharges++;
            Destroy(gameObject);
        }
    }
}
