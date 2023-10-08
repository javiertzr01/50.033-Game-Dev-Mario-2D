using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.gameRestart.AddListener(GameRestart);
        GameManager.instance.goombaDie.AddListener(GoombaDie);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameRestart()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<EnemyMovement>().GameRestart();
        }
    }

    public void GoombaDie(string name)
    {
        transform.Find(name).GetComponent<EnemyMovement>().Die();
    }
}
