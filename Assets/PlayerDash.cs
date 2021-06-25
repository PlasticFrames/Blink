using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash: MonoBehaviour
{
    PlayerMove moveScript;

    public Camera runCam;

    public GameObject dashAim;
    public GameObject dashMark;

    public List<Vector3> dashMarks = new List<Vector3>();

    public float groundZ = 0f;
    public float distance;
    public float distanceFromPlayer;
    public float maxDistance = 15f;
    public float dashSpeed;

    public int dashCharges = 3;
    public int maxDash = 3;
    public int currentDash = 0;

    public Vector3 aimOrigin;
    public Vector3 dashDestination;

    public bool isPlanning;
    public bool isDashing;

    void Start()
    {
        moveScript = GetComponent<PlayerMove>();
        runCam = Camera.main;
        dashAim = GameObject.FindWithTag("Dash Aim");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanning && dashCharges > 0)
        {
            isPlanning = true;
            aimOrigin = transform.position;
            dashAim.SetActive(true);
            LimitRange();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isPlanning)
        {
            isPlanning = false;
            isDashing = true;
            StartCoroutine(TriggerDashes());
        }

        if (Input.GetMouseButtonDown(0) && isPlanning)
        {
            dashDestination = dashAim.transform.position;
            SetDestinations();
        }

        if (isPlanning)
        {
            LimitRange();
        }
        else if (!isPlanning)
        {
            dashAim.SetActive(false);
        }

        if (isPlanning || isDashing)
        {
            moveScript.enabled = false;
        }
        else if (!isPlanning && !isDashing)
        {
            moveScript.enabled = true;
        }
    }

    public Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = runCam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0, z, 0));
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }

    void LimitRange()
    {
        distanceFromPlayer = Vector3.Distance(GetWorldPosition(groundZ), transform.position);
        Vector3 offset = GetWorldPosition(groundZ) - aimOrigin;
        dashAim.transform.position = aimOrigin + Vector3.ClampMagnitude(offset, maxDistance);
    }

    void SetDestinations() //DISABLE AIM VS MAX DASH?
    {
        if (dashCharges > 0)
        {
            Instantiate(dashMark, dashDestination, Quaternion.identity);
            aimOrigin = dashDestination;
            currentDash++;
            dashCharges--;
        }
    }

    IEnumerator TriggerDashes()
    {
        currentDash = 0;
        foreach (var Vector3 in dashMarks)
        {
            yield return LerpDash (Vector3, dashSpeed);
            currentDash++;
        }
        dashMarks.Clear();
        dashCharges = maxDash;
        currentDash = 0;
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
        transform.position = targetPos; //snaps to next position
    }
} 