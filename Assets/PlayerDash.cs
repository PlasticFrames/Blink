using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash: MonoBehaviour
{
    PlayerMove moveScript;

    public Camera cam;

    public GameObject dashAim;

    [SerializeField] GameObject[] dashMarks = new GameObject[3];

    public float groundZ = 0f;
    public float distance;
    public float distanceFromPlayer;
    public float maxDistance = 10f;
    public float dashSpeed;
    public float dashTime;

    public int dashNumber = 0;
    public int maxDash = 3;

    public Vector3 dashOrigin;
    public Vector3 dashDestination;

    public bool isPlanning = false;

    /*public float smoothTime = 0.3F; //attempted smooth damp
    private Vector3 velocity = Vector3.zero;*/

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanning)
        {
            isPlanning = true;
            dashOrigin = transform.position;
            LimitRange();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isPlanning)
        {
            isPlanning = false;
            TriggerDashes();
            //StartCoroutine(OldDash());
        }

        if (isPlanning)
        {
            LimitRange();
        }
        else if (!isPlanning)//move into exit plan function?
        {
            dashNumber = 0;
            dashAim.SetActive(false); 
        }

        if(Input.GetMouseButtonDown(0) && isPlanning)
        {
            dashDestination = dashAim.transform.position;
            SetDestinations();
        }
    }

    public Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0, z, 0));
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }

    void LimitRange()
    {
        distanceFromPlayer = Vector3.Distance(GetWorldPosition(groundZ), transform.position);
        Vector3 offset = GetWorldPosition(groundZ) - dashOrigin;
        dashAim.transform.position = dashOrigin + Vector3.ClampMagnitude(offset, maxDistance);
        dashAim.SetActive(true);
    }

    void SetDestinations()
    {
        if (dashNumber < 3)
        {
            dashMarks[dashNumber].transform.position = dashDestination;
            dashMarks[dashNumber].SetActive(true);
            dashOrigin = dashMarks[dashNumber].transform.position;
            dashNumber++;
        }
    }
    
    void TriggerDashes()
    {
        dashNumber = 0;

        foreach (GameObject gameObject in dashMarks)
        {
            if (dashMarks[dashNumber].transform.position != Vector3.zero)
            {
                //transform.position = dashMarks[dashNumber].transform.position;
                StartCoroutine(DashMovement());
                //transform.position = Vector3.Lerp(transform.position, dashMarks[dashNumber].transform.position, 1);
                //transform.position = Vector3.SmoothDamp(transform.position, dashMarks[dashNumber].transform.position,  ref velocity, smoothTime);
                dashMarks[dashNumber].SetActive(false);
                dashMarks[dashNumber].transform.position = Vector3.zero; //reset transform for next dash - MOVE TO OWN SCRIPT?
                dashNumber++;
            }
        }
    }

    IEnumerator DashMovement()
    {
        float startTime = Time.time;

        transform.LookAt(dashMarks[dashNumber].transform.position);
        moveScript.moveDir = (dashMarks[dashNumber].transform.position - transform.position).normalized; //manually sets direction
        //Debug.Log("moveDir manually set to " + moveScript.moveDir);

        while(Time.time < startTime + dashTime)
        {
            Debug.Log("moveDir pre-dash = " + moveScript.moveDir);
            moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);
            Debug.Log("moveDir post-dash = " + moveScript.moveDir);
            yield return null;
        }
    }

    IEnumerator OldDash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            
            moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);//moveDir - playerPos.normalized instead?

            yield return null;
            
        }
    }
} 