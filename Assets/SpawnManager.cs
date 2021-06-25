using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject baseEnemy;

    public Vector3 enemyPosition;

    public Quaternion enemyRotation;
    
    public void SpawnBase()
    {
        Debug.Log("Base spawned?");
        Instantiate(baseEnemy, Vector3.zero, enemyRotation);
    }
}
