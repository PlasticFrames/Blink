using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DashRecharge : MonoBehaviour
{
    public PlayerDash dashScript;

    public Color inactiveColour = new Color (255, 0 , 0, 128);
    public Color activeColour = new Color (0, 255, 255, 128);
    public Material materialToChange; 
    public Rigidbody rechargeBody;

    [SerializeField] float rechargeForce;

    public bool isActive = false;

    void Start()
    {
        materialToChange = GetComponent<Material>();
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        rechargeBody = GetComponent<Rigidbody>();

        rechargeBody.AddForce(Random.insideUnitCircle.normalized * rechargeForce, ForceMode.Impulse);
        StartCoroutine(DelayActivation());
        //StartCoroutine(LerpColour(activeColour, 5));
    }

    IEnumerator DelayActivation() //Stops player collecting charge immediately
    {
        yield return new WaitForSeconds(0.5f);
        isActive = true;
    }

    void OnTriggerEnter(Collider other)  //Recharges dashes on collision
    {
        if (other.gameObject.CompareTag ("Player") && isActive)
        {
            dashScript.maxDash++;
            dashScript.dashCharges++;
            Destroy(gameObject);
        }
    }

    /*IEnumerator LerpColour(Color endValue, float duration) //Lerps colour to illustrate delay
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
    }*/
}
