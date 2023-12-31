using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePowerupWeek5 : MonoBehaviour, IPowerup
{
    public PowerupType type;
    public bool spawned = false;
    protected bool consumed = false;
    protected bool goRight = true;
    protected Rigidbody2D rigidBody;
    public UnityEvent<IPowerup> powerupAffectsX;

    // base methods

    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // interface methods
    // 1. concrete methods
    public PowerupType powerupType
    {
        get
        {
            return type;
        }
    }

    public bool hasSpawned
    {
        get
        {
            return spawned;
        }
    }

    public void PowerupCollected(IPowerup i)
    {
        powerupAffectsX.Invoke(i);
    }

    public void DestroyPowerup()
    {
        // this.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    // 2. abstract methods, must be implemented by derived classes
    public virtual void SpawnPowerup()
    {
        spawned = true;
    }
    public abstract void ApplyPowerup(MonoBehaviour i);
    public virtual void GameRestart()
    {
        spawned = false;
    }
}
