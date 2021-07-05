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
    public GameObject particleParent;

    public Rigidbody playerBody;

    public List<Vector3> dashMarks = new List<Vector3>();

    public float groundZ = 0f;
    public float distance;
    public float distanceFromPlayer;
    public float maxDistance = 15f;
    public float dashSpeed;
    private float raycastLength = 500f;

    public int dashCharges = 3;
    public int maxDash = 3;
    public int currentDash = 0;
    public int dashDelay = 1;

    public Vector3 aimOrigin;
    public Vector3 dashDestination;

    public bool isPlanning;
    public bool isDashing;

    public ParticleSystem dashAimSparks;
    public ParticleSystem dashAimBeam;
    public ParticleSystem dashAimReticuleBlue;
    public ParticleSystem dashAimReticuleRed;

    RaycastHit hit;

    void Start()
    {
        moveScript = GetComponent<PlayerMove>();
        runCam = Camera.main;
        playerBody = GetComponent<Rigidbody>();
        dashAim = GameObject.FindWithTag("Dash Aim");
    }

    void Update()
    {
        var dashAimSparksEmission = dashAimSparks.emission;
        var dashAimBeamEmission = dashAimBeam.emission;
        var dashAimReticuleBlueEmission = dashAimReticuleBlue.emission;
        var dashAimReticuleRedEmission = dashAimReticuleRed.emission;

        if (Input.GetKeyDown(KeyCode.Space) && !isPlanning && !isDashing && dashCharges > 0)
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
            dashAimReticuleBlue.Play();
            dashAimReticuleRed.Play();
            dashAimSparks.Play();
            dashAimBeam.Play();
            SetDestinations();
        }
        
        if(currentDash == 3)
        {
            dashAimReticuleRed.Play();
            dashAimReticuleBlue.Stop();
            dashAimReticuleBlueEmission.enabled = false;
            dashAimReticuleRedEmission.enabled = true;
            dashAimSparksEmission.enabled = false;
            dashAimBeamEmission.enabled = false;
        }
        else
        {
            dashAimReticuleRed.Stop();
            dashAimReticuleBlue.Play();
            dashAimReticuleBlueEmission.enabled = true;
            dashAimReticuleRedEmission.enabled = false;
            dashAimSparksEmission.enabled = true;
            dashAimBeamEmission.enabled = true;
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
            playerBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            moveScript.enabled = false;
        }
        else if (!isPlanning && !isDashing)
        {
            playerBody.constraints = ~RigidbodyConstraints.FreezePosition;
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
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, raycastLength))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.tag == "Ground")
            {
                dashAim.transform.position = hit.point;
                Vector3 offset = hit.point - aimOrigin;
                dashAim.transform.position = aimOrigin + Vector3.ClampMagnitude(offset, maxDistance);
                dashAim.SetActive(true); //DISABLE AIM VS MAX DASH?
                particleParent.transform.rotation = Quaternion.LookRotation(hit.normal);

            }
        }
        /*
        distanceFromPlayer = Vector3.Distance(GetWorldPosition(groundZ), transform.position);
        Vector3 offset = GetWorldPosition(groundZ) - aimOrigin;
        dashAim.transform.position = aimOrigin + Vector3.ClampMagnitude(offset, maxDistance);
        dashAim.SetActive(true); //DISABLE AIM VS MAX DASH?
        */
    }

    void SetDestinations() //Instantiates mark at aim position and increments charges
    {
        if (dashCharges > 0)
        {
            Instantiate(dashMark, dashDestination, Quaternion.identity);
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
            yield return LerpDash (Vector3, dashSpeed);
            currentDash++;
        }
        dashMarks.Clear();
        currentDash = 0;
        isDashing = false;
        StartCoroutine(CooldownDashes());
    }

    IEnumerator CooldownDashes()
    {
        if (dashCharges < maxDash)
        {
            yield return new WaitForSeconds(dashDelay);
            dashCharges++;
            yield return CooldownDashes();
        }
        else
        {
            dashCharges = maxDash;
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