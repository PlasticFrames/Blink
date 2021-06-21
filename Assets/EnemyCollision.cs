using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    PlayerMove moveScript;

    public GameObject player;  
    public Rigidbody playerBody;

    [SerializeField] float nudgeForce;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("Enemy hit");
    
        if (other.gameObject.tag == "Player")
        {
            playerBody.AddForce((transform.position * -1) * nudgeForce);
        }
    }
}
