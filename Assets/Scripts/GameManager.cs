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

    public bool isComplete = true;

    void Start() 
    {
        finish = GameObject.FindWithTag("Finish").gameObject.transform.GetChild(0).gameObject;
    }

    void Update() 
    {
        CountEnemies();
        Debug.Log(enemyCount);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckScene();
        }

        if(enemyCount <= 0)
        {
            finish.SetActive(true);
        }
    }

    void CheckScene()
    {
        currentScene = SceneManager.GetActiveScene();
        buildIndex = currentScene.buildIndex;

        if (buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    void CountEnemies()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
