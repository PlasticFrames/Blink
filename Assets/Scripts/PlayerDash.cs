using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash: MonoBehaviour
{
    PlayerMove moveScript;
    PlayerHealth healthScript;

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
    public int dashDelay = 1;

    public Vector3 aimOrigin;
    public Vector3 dashDestination;

    public bool isPlanning;
    public bool isDashing;

    void Start()
    {
        moveScript = GetComponent<PlayerMove>();
        healthScript = GetComponent<PlayerHealth>();
        runCam = Camera.main;
        dashAim = GameObject.FindWithTag("Dash Aim");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanning && !isDashing && dashCharges > 0) //SWAP TO RMB?
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

    public Vector3 GetWorldPosition(float z) //Retrieves mouse position on ground
    {
        Ray mousePos = runCam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0, z, 0));
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }

    void LimitRange() //Displays dash aim and clamps to player/mark
    {
        distanceFromPlayer = Vector3.Distance(GetWorldPosition(groundZ), transform.position);
        Vector3 offset = GetWorldPosition(groundZ) - aimOrigin;
        dashAim.transform.position = aimOrigin + Vector3.ClampMagnitude(offset, maxDistance);
        dashAim.SetActive(true); //DISABLE AIM VS MAX DASH?
    }

    void SetDestinations() //Instantiates mark at aim position and increments charges
    {
        if (dashCharges > 0)
        {
            Instantiate(dashMark, dashDestination, Quaternion.identity);
            aimOrigin = dashDestination;
            currentDash++;
            dashCharges--;
        }
    }

    IEnumerator TriggerDashes() //Resets int for loop, triggers dashes and clears marks
    {
        currentDash = 0;
        foreach (var Vector3 in dashMarks)
        {
            yield return LerpDash(Vector3, dashSpeed);
            currentDash++;
        }
        dashMarks.Clear();
        currentDash = 0;
        isDashing = false;
        StartCoroutine(healthScript.MakeInvulnerable());
        StartCoroutine(DelayDashes());
    }

    IEnumerator DelayDashes() //Slowly recharges dashes
    {
        if (dashCharges < maxDash)
        {
            yield return new WaitForSeconds(dashDelay);
            dashCharges++;
            yield return DelayDashes();
        }
        else
        {
            yield return null;
        }
    }

    IEnumerator LerpDash(Vector3 targetPos, float duration) //Lerps through dashes
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