using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyTrigger triggerScript;
    public PlayerDash dashScript;

    public GameObject player;

    [SerializeField] public float rotationSpeed;

    [SerializeField] public Vector3 yAngle;

    void Start()
    {
        triggerScript = GetComponent<EnemyTrigger>();
        dashScript = GameObject.FindWithTag("Player").GetComponent<PlayerDash>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {

        if (triggerScript.isIdle)
        {
            transform.Rotate((yAngle * rotationSpeed) * Time.deltaTime);
        }
        else if (triggerScript.isMoving || triggerScript.isAttacking) //enemy is facing player?
        {
            Vector3 forward = player.transform.position - transform.position;
            forward.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forward), rotationSpeed * Time.deltaTime);
        }
    }
}
