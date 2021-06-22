using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public PlayerDash dashScript;

    public Collider enemyCollider;
    public GameObject player;  
    public Rigidbody playerBody;

    [SerializeField] float nudgeForce;
    [SerializeField] float nudgeRadius;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("Enemy hit");

        if (other.gameObject.CompareTag ("Player") && dashScript.isDashing == false)
        {
            playerBody.AddExplosionForce(nudgeForce, transform.position, nudgeRadius); // TIE TO UNIVERSAL FORCE VARIABLE?
        }
        else if (other.gameObject.CompareTag ("Player") && dashScript.isDashing == true)
        {
            Destroy(gameObject);
        }
    }
}
