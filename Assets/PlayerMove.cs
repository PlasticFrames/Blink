using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody playerBody;
    public Transform runCam;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    public Vector3 moveDirection;
    Vector3 velocity;

    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        runCam = Camera.main.transform;
        //Cursor.lockState = CursorLockMode.Locked; //Seems to help camera control
        GetComponent<Rigidbody>().velocity = velocity * Time.deltaTime;
    }
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 dir = new Vector3(h, 0f, v).normalized;

        if(dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + runCam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerBody.MovePosition(transform.position + input * Time.deltaTime * speed);
        }
    }
}
