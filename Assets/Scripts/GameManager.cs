using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

{
    public Scene currentScene;

    public int buildIndex;
    public int enemyCount;

    public float finishDelay = 2f;

    public bool isComplete = false;

    void Update() 
    {
        CheckScene();

        if(buildIndex == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.FindObjectOfType<AudioManager>().Play("Dash");
            ChooseScene();
        }

        if(buildIndex > 0)
        {
            CountEnemies();
            Debug.Log(enemyCount);
        }

        if(isComplete)
        {
            StartCoroutine(DelayFinish());
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
            isComplete = true;
        }
    }
    
    IEnumerator DelayFinish()
    {
        yield return new WaitForSeconds(finishDelay);
        ChooseScene();
    }

    public void ChooseScene()
    {
        if (buildIndex < 2)
        {
            isComplete = false;
            SceneManager.LoadScene(buildIndex + 1);
        }
        else if (buildIndex > 1 && isComplete)
        {
            SceneManager.LoadScene(0);
        }
    }
}
