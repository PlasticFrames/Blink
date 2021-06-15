using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    public Vector3 playerPos;
    public Vector3 dashDirection;
    public Vector3 mousePosition;
    public Vector3 debugCubePos;
    public Vector3 dashPosition;
    public Camera cam;
    public float groundZ = 0f;
    public float ease;
    public float speed = 0.5f;
    public float distance;
    public float distanceFromPlayer;
    public float maxDistance = 20f;

    public GameObject captainCapsule;
    public GameObject debugCube;

    public bool isLerping;

    private float fraction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(GetWorldPosition(groundZ), captainCapsule.transform.position);
        playerPos = captainCapsule.transform.position;
        debugCubePos = debugCube.transform.position;
        dashDirection = playerPos - GetWorldPosition(groundZ);

        Vector3 offSet = GetWorldPosition(groundZ) - playerPos;
        debugCube.transform.position = playerPos + Vector3.ClampMagnitude(offSet, maxDistance);


        fraction += Time.deltaTime * speed;

        if(distanceFromPlayer < maxDistance)
        {
            dashPosition = debugCube.transform.position;
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            dashDirection = new Vector3(0, 0, 0);
            isLerping = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ease = 5f;
            isLerping = false;
            playerPos = GetWorldPosition(groundZ);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isLerping = true;
            ease = ease - Time.deltaTime * 2f;
        }

        if (isLerping == true)
        {
            ease = ease - (Time.deltaTime * 2f) * 3f;
        }
        else ease = 5f;

        if (ease <= 0f)
        {
            isLerping = false;
            dashDirection = new Vector3(0, 0, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            //dashDirection = playerPos - GetWorldPosition(groundZ);
            //captainCapsule.transform.position = debugCube.transform.position;
            captainCapsule.transform.position = Vector3.Lerp(captainCapsule.transform.position, debugCube.transform.position, fraction);

        }









        mousePosition = GetWorldPosition(groundZ);

    }



    public Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0, z, 0));
        ground.Raycast(mousePos, out distance);
        //mousePosition = mousePos;
        return mousePos.GetPoint(distance);
    }
}
