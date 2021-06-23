using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash: MonoBehaviour
{
    PlayerMove moveScript;

    public Camera runCam;

    public GameObject dashAim;

    [SerializeField] GameObject[] dashMarks = new GameObject[3];

    public Renderer aimRenderer;

    public float groundZ = 0f;
    public float distance;
    public float distanceFromPlayer;
    public float maxDistance = 10f;
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanning && dashCharges > 0)
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
        else if (!isPlanning)
        {
            ShowAim();
            aimRenderer.enabled = false;
        }

        if (Input.GetMouseButtonDown(0) && isPlanning)
        {
            dashDestination = dashAim.transform.position;
            SetDestinations();
        }
    }

    private void ShowAim()
    {
        dashAim.SetActive(false);
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
        dashAim.SetActive(true);
        aimRenderer.enabled = true;
    }

    void SetDestinations() //DISABLE AIM VS MAX DASH?
    {
        if (dashCharges > 0)
        {
            dashMarks[currentDash].transform.position = dashDestination;
            dashMarks[currentDash].SetActive(true);
            aimOrigin = dashMarks[currentDash].transform.position;
            currentDash++;
            dashCharges--;
        }
    }

    IEnumerator TriggerDashes()
    {
        currentDash = 0;
        foreach (GameObject gameObject in dashMarks)
        {
            if (dashMarks[currentDash].transform.position != Vector3.zero)
            {
                yield return LerpDash (dashMarks[currentDash].transform.position, dashSpeed);
                dashMarks[currentDash].SetActive(false);
                dashMarks[currentDash].transform.position = Vector3.zero; //reset position to limit next dashes
                currentDash++;
            }
        }
        currentDash = 0;
        dashCharges = maxDash;
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