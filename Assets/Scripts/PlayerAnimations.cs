using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator playerAnim;
    public GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
            
        }

        if (playerObj.gameObject.GetComponent<PlayerDash>().isDashing == true)
        {
            playerAnim.SetBool("isDashing", true);
        }
        else
        {
            playerAnim.SetBool("isDashing", false);
        }

    }
}
