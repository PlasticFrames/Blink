using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Scene currentScene;
    public int buildIndex;

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckScene();
        }
    }

    void CheckScene()
    {
        currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;

        if (buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
