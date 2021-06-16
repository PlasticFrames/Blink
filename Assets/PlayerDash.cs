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
    
    public float groundZ = 0f;
    public float distance;
    public float distanceFromPlayer;
    public float maxDistance = 10f;
    public float dashSpeed;
    public float dashTime;

    public int dashNumber = 0;
    [SerializeField] GameObject[] dashDestinations = new GameObject[3];

    public Vector3 playerPosition;
    public Vector3 mousePosition;
    public Vector3 dashDirection;
    public Vector3 dashDestination;
    public Vector3 destination1;
    public Vector3 destination2;
    public Vector3 destination3;

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
            
            playerPosition = transform.position;
        }

        if (isPlanning)
        {
            dashAim.SetActive(true);
            Plan();
        }
        else
        {
            dashAim.SetActive(false);
            //Destroy(GameObject.FindWithTag("Marker"));
            dashNumber = 0;
        }

        if(Input.GetMouseButtonDown(0) && isPlanning)
        {
            mousePosition = GetWorldPosition(groundZ);
            SetDestination();
            //StartCoroutine(Dash());
        }

        if (distanceFromPlayer < maxDistance) //WHATTTTTTTTTTT
        {
            dashDestination = dashAim.transform.position;
        }
    }

    public Vector3 GetWorldPosition(float z) //
    {
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0, z, 0));
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }

    void Plan()
    {
        //dashAim.transform.position = mousePosition;

        distanceFromPlayer = Vector3.Distance(GetWorldPosition(groundZ), transform.position);
        dashDirection = playerPosition - GetWorldPosition(groundZ);
        Vector3 offset = GetWorldPosition(groundZ) - playerPosition;
        dashAim.transform.position = playerPosition + Vector3.ClampMagnitude(offset, maxDistance);
    }

    void SetDestination() //find more efficient way to cycle!
    {
        dashDestinations[dashNumber].transform.position = mousePosition;
        //Instantiate(dashDestinations[dashNumber], mousePosition, Quaternion.identity);
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
/*
1. store player position /
2. clamp range /
3. first dash /
4. trigger
5. stack second dash /
6. stack third dash /
7. undo dash
*/ 
