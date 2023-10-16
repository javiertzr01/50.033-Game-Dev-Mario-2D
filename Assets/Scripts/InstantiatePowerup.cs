using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePowerup : MonoBehaviour
{
    public GameObject Powerup;
    
    public void GameRestart()
    {
        if (gameObject.transform.Find(Powerup.name) == null && gameObject.transform.Find(Powerup.name + "(Clone)") == null)
        {
            Instantiate(Powerup, transform);
        }
    }
}
