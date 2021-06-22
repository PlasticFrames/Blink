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

    public int dashNumber = 0;
    public int maxDash = 3;

    public Vector3 aimOrigin;
    public Vector3 dashDestination;

    public bool isPlanning = false;
    public bool isDashing = false;

    void Start()
    {
        moveScript = GetComponent<PlayerMove>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanning)
        {
            isPlanning = true;
            aimOrigin = transform.position;
            LimitRange();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isPlanning)
        {
            isPlanning = false;
            isDashing = true;
            StartCoroutine(TriggerDashes());
        }

        if (isPlanning)
        {
            LimitRange();
        }
        else if (!isPlanning) //MOVE INTO EXIT PLAN FUNCTION?
        {
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
        Vector3 offset = GetWorldPosition(groundZ) - aimOrigin;
        dashAim.transform.position = aimOrigin + Vector3.ClampMagnitude(offset, maxDistance);
        dashAim.SetActive(true);
    }

    void SetDestinations()
    {
        if (dashNumber < 3)
        {
            dashMarks[dashNumber].transform.position = dashDestination;
            dashMarks[dashNumber].SetActive(true);
            aimOrigin = dashMarks[dashNumber].transform.position;
            dashNumber++;
        }
    }

    IEnumerator TriggerDashes()
  {
    dashNumber = 0;
    foreach (GameObject gameObject in dashMarks)
    {
        if (dashMarks[dashNumber].transform.position != Vector3.zero)
        {
            yield return LerpDash (dashMarks[dashNumber].transform.position, dashSpeed);
            dashMarks[dashNumber].SetActive(false);
            dashMarks[dashNumber].transform.position = Vector3.zero; //reset position to limit next dashes
            dashNumber++;
        }
    }
    dashNumber = 0;
    isDashing = false;
  }

    IEnumerator LerpDash(Vector3 targetPos, float duration) //still seems to be smoothing between points?
    {
        float time = 0;
        Vector3 startPos = transform.position;
        while (time < duration)
        {
            transform.LookAt(targetPos);
            transform.position = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos; //snapping
        
    }
} 