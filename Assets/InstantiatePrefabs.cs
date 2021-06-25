using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefabs : MonoBehaviour
{
    public GameObject player;
    public GameObject dashRecharge;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        dashRecharge = GameObject.FindWithTag("Dash Recharge");

        StartCoroutine(DestroyPrefabs());
        DestroyPrefabs();
        //Instantiate(player, Vector3.zero, Quaternion.identity);       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyPrefabs()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(GameObject.FindWithTag("Dash Recharge"));
    }
}
