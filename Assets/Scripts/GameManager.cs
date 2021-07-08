using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

{
    public TimeManager timeScript;
    public GameObject finish;
    public Scene currentScene;

    public int buildIndex;
    public int enemyCount;

    public bool isComplete = false;

    void Start() 
    {
        currentScene = SceneManager.GetActiveScene();
        buildIndex = currentScene.buildIndex;
        finish = GameObject.FindWithTag("Finish").gameObject.transform.GetChild(0).gameObject;
    }

    void Update() 
    {
        CountEnemies();
        Debug.Log(enemyCount);

        if(Input.GetKeyDown(KeyCode.Space) && buildIndex == 0)
        {
            isComplete = true;
            CheckScene();
        }

        if(enemyCount <= 0)
        {
            finish.SetActive(true);
        }

        if(buildIndex == 0)
        {
            timeScript.enabled = false;
        }
        else
        {
            timeScript.enabled = true;
        }
    }

    void CountEnemies()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void CheckScene()
    {
        currentScene = SceneManager.GetActiveScene();
        buildIndex = currentScene.buildIndex;

        if (isComplete && buildIndex < 2)
        {
            SceneManager.LoadScene(buildIndex + 1);
            isComplete = false;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        currentScene = SceneManager.GetActiveScene();
        buildIndex = currentScene.buildIndex;
    }
}
