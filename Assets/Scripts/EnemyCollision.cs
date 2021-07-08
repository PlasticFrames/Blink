using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCollision : MonoBehaviour
{
    public PlayerDash dashScript;
    public DashRecharge rechargeScript;
    public EnemySwitch switchScript;
    public EnemyTrigger triggerScript;

    public GameObject player;
    public GameObject dashRecharge;
    public GameObject bulletOffset;
    public GameObject explosion;
    public NavMeshAgent agent;        
    public Rigidbody playerBody;
    public Rigidbody enemyBody;

    public int destroyDelay = 1;

    public float nudgeForce = 100f;
    public float knockMultiplier = 2f;    
    public float reactionRadius = 2f;

    public Vector3 forceOrigin;

    void Start() 
    {
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        switchScript = GetComponent<EnemySwitch>();
        triggerScript = GetComponent<EnemyTrigger>();
        player = GameObject.FindWithTag("Player");
        bulletOffset = (gameObject.transform.GetChild(1).gameObject);
        explosion = GameObject.FindWithTag("Explosion");
        agent = GetComponent<NavMeshAgent>();
        playerBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        enemyBody = GetComponent<Rigidbody>();       
    }

    void Update() 
    {
        if (dashScript.isDashing)
        {
            agent.enabled = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (dashScript.isDashing == false)
            {
                RunReaction();
            }
            else if (dashScript.isDashing == true)
            {
                DashReaction();
            }
        }
    }

    void RunReaction()
    {
        forceOrigin = transform.position;

        switch (switchScript.enemyType)
        {
            case 0:
                playerBody.AddExplosionForce(nudgeForce, forceOrigin, reactionRadius, 0, ForceMode.Impulse);
                break;
            case 1:
                playerBody.AddExplosionForce(nudgeForce * knockMultiplier, forceOrigin, reactionRadius, 0, ForceMode.Impulse); //ADD PLAYER INPUT DISABLE?
                break;
            case 2:
                playerBody.AddExplosionForce(nudgeForce * knockMultiplier, forceOrigin, reactionRadius, 0, ForceMode.Impulse);
                dashScript.dashCharges--;
                dashScript.maxDash--;

                if (dashScript.dashCharges >= 0)
                {
                    Instantiate(dashRecharge, player.transform.position + (Vector3.up * 2), Quaternion.identity);
                }
                break;
        }
    }

    void DashReaction()
    {
        forceOrigin = player.transform.position;
        triggerScript.isPushed = true;

        switch (switchScript.enemyType)
        {
            case 0:
                enemyBody.AddExplosionForce(nudgeForce * 2, forceOrigin, reactionRadius, 0, ForceMode.Impulse);
                StartCoroutine(DelayDestroy());
                break;
            case 1:
                enemyBody.AddExplosionForce(nudgeForce * 3, forceOrigin, reactionRadius, 0, ForceMode.Impulse);
                switchScript.enemyType = 0;
                switchScript.CheckType();
                break;
            case 2:
                enemyBody.AddExplosionForce(nudgeForce, forceOrigin, reactionRadius, 0, ForceMode.Impulse);
                break;
        }
    }

    public IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(destroyDelay);
        Instantiate(explosion, bulletOffset.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
