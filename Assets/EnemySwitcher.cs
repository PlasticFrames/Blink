using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwitcher : MonoBehaviour
{
    EnemyCollision collisionScript;

    [SerializeField] public int enemyType; //1 = Base, 2 = shield, 3 = armour

    void Start() 
    {
        collisionScript = GetComponent<EnemyCollision>();
    }

    void Update() //NEED TO FIND A WAY TO ENABLE INACTIVE CHILD!
    {
        switch (enemyType)
        {
        case 0: 
            GameObject.FindWithTag("Base").SetActive(true);
            GameObject.FindWithTag("Shield").SetActive(false);
            GameObject.FindWithTag("Armour").SetActive(false);
            break;
        case 1:
            GameObject.FindWithTag("Base").SetActive(false);
            GameObject.FindWithTag("Shield").SetActive(true);
            GameObject.FindWithTag("Armour").SetActive(false);
            break;
        case 2:
            GameObject.FindWithTag("Base").SetActive(false);
            GameObject.FindWithTag("Shield").SetActive(false);
            GameObject.FindWithTag("Armour").SetActive(true);
            break;
        }
    }
}
