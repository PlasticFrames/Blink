using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

{
    public GameObject finish;
    public Scene currentScene;

    public int buildIndex;
    public int enemyCount;

    public bool isComplete = false;

    void Start() 
    {
        finish = GameObject.FindWithTag("Finish").gameObject.transform.GetChild(0).gameObject;
    }

    void Update() 
    {
        CheckScene();

        if(buildIndex == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            ChooseScene();
        }

        if(buildIndex > 0)
        {
            CountEnemies();
        }

        if(isComplete)
        {
            //ChooseScene();
        }
    }

    public void CheckScene()
    {
        currentScene = SceneManager.GetActiveScene();
        buildIndex = currentScene.buildIndex;
    }

    public void CountEnemies()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        
        if(enemyCount <= 0)
        {
            finish.SetActive(true);
            isComplete = true;
        }
    }

    public void ChooseScene()
    {
        if (buildIndex < 2)
        {
            SceneManager.LoadScene(buildIndex + 1);
            isComplete = false;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
