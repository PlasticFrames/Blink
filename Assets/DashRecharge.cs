using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRecharge : MonoBehaviour
{
    public bool isActive = false;

    public Color inactiveColour = new Color (255, 0 , 0, 128);
    public Color activeColour = new Color (0, 255, 255, 128);

    public Material materialToChange; 

    public PlayerDash dashScript;

    public Rigidbody rechargeBody;

    public Vector3 rechargeDirection;

    [SerializeField] float rechargeForce;

    void Start()
    {
        rechargeBody.AddForce(rechargeDirection * rechargeForce, ForceMode.Impulse);
        StartCoroutine(DelayActivation());
        //StartCoroutine(LerpColour(activeColour, 5));
    }

    void OnEnable() 
    {

    }

    IEnumerator DelayActivation()
    {
        yield return new WaitForSeconds(0.5f);
        isActive = true;
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ("Player") && isActive)
        {
            dashScript.maxDash++;
            dashScript.dashCharges++;
            Destroy(gameObject);
        }
    }

    IEnumerator LerpColour(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = materialToChange.color;

        while (time < duration)
        {
            materialToChange.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        materialToChange.color = endValue;
    }
}
