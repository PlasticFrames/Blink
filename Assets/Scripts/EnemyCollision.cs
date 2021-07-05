using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCollision : MonoBehaviour
{
    public PlayerDash dashScript;
    public DashRecharge rechargeScript;
    public EnemySwitch switchScript;

    public GameObject player;
    public GameObject dashRecharge;
    public NavMeshAgent agent;        
    public Rigidbody playerBody;
    public Rigidbody enemyBody;

    [SerializeField] float nudgeForce;
    public float knockMultiplier = 2f;    
    public float reactionRadius = 2f;

    public Vector3 forceOrigin;

    void Start() 
    {
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        switchScript = GetComponent<EnemySwitch>();
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        playerBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        enemyBody = GetComponent<Rigidbody>();       
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
        //playerBody.drag = 5;

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
       // agent.enabled = false;
        //enemyBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

        switch (switchScript.enemyType)
        {
            case 0:
                Destroy(gameObject);
                break;
            case 1:
                switchScript.enemyType = 0;
                switchScript.CheckType();
                break;
            case 2:
                Debug.Log("Nudge armour");
                enemyBody.AddExplosionForce(nudgeForce, forceOrigin, reactionRadius, 0, ForceMode.Impulse);//SWAP TO LERP? RENABLING MOVEMENT MIGHT HELP
                break;
        }
        //StartCoroutine(ResetBody());
    }

    IEnumerator ResetBody()
    {
        yield return new WaitForSeconds(0.5f);
        enemyBody.constraints = ~RigidbodyConstraints.FreezePosition;
    }
}
