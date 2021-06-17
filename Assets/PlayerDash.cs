using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash: MonoBehaviour
{
    PlayerMove moveScript;

    public Camera cam;

    public GameObject dashAim;

    [SerializeField] GameObject[] dashWaypoints = new GameObject[3];

    public float groundZ = 0f;
    public float distance;
    public float distanceFromPlayer;
    public float maxDistance = 10f;
    public float dashSpeed;
    public float dashTime;

    public int dashNumber = 0;

    public Vector3 dashOrigin;
    public Vector3 dashDestination;

    public bool isPlanning = false;

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
            Dash();
        }

        if (isPlanning)
        {
            LimitRange();
        }
        else if (!isPlanning)//move into exit plan function?
        {
            dashNumber = 0;
            dashAim.SetActive(false);
            //dashWaypoints[dashNumber].SetActive(false); //not deactivating or reactivating 
        }

        if(Input.GetMouseButtonDown(0) && isPlanning)
        {
            dashDestination = dashAim.transform.position;
            SetDestinations();
            //StartCoroutine(Dash());
        }
    }

    public Vector3 GetWorldPosition(float z) //monitors mouse position
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
        dashWaypoints[dashNumber].transform.position = dashDestination;
        dashWaypoints[dashNumber].SetActive(true);
        dashOrigin = dashWaypoints[dashNumber].transform.position;
        dashNumber++;
    }
    
    void Dash()
    {

    }

    /*IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);

            yield return null;
            //moveDir - playerPos.normalized
        }
    }*/
} 