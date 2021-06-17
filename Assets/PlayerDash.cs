using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash: MonoBehaviour
{
    PlayerMove moveScript;

    public Camera cam;

    public GameObject dashAim;
    public GameObject dashMark;

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
    public Vector3 dashDirection;

    public Quaternion dashRotation;

    public bool isPlanning;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlanning = !isPlanning;
            dashOrigin = transform.position;
        }

        if (isPlanning)
        {
            Plan();
        }
        else //move into exit plan function?
        {
            dashAim.SetActive(false);
            dashMark.SetActive(false);
            dashNumber = 0;
        }

        if(Input.GetMouseButtonDown(0) && isPlanning)
        {
            dashDestination = dashAim.transform.position;
            SetDestination();
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

    void Plan()
    {
        distanceFromPlayer = Vector3.Distance(GetWorldPosition(groundZ), transform.position);
        //dashDirection = dashOrigin - GetWorldPosition(groundZ);
        Vector3 offset = GetWorldPosition(groundZ) - dashOrigin;
        dashAim.transform.position = dashOrigin + Vector3.ClampMagnitude(offset, maxDistance);
        dashAim.SetActive(true);
    }

    void SetDestination()
    {
        //dashWaypoints[dashNumber].transform.position = dashDestination;
        //dashWaypoints[dashNumber].SetActive(true);
        Instantiate(dashWaypoints[dashNumber], dashDestination, Quaternion.identity);
        dashNumber++;
    }
    
    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);

            yield return null;
            //moveDir - playerPos.normalized
        }
    }
} 