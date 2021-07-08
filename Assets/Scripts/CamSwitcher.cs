using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitcher : MonoBehaviour
{

    public Camera mainCam;
    public Camera screenshotCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam.enabled = true;
        screenshotCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            mainCam.enabled = !mainCam.enabled;
            screenshotCam.enabled = !screenshotCam.enabled;
        }
    }
}
