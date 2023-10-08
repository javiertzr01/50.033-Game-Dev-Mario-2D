using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    // lives
    public int maxLives;

    // Mario's movement
    public int speed;
    public int maxSpeed;
    public int upSpeed;
    public int deathImpulse;
    public Vector3[] spawnLocations = {
        new Vector3 (-8.26f, -4.22f, 0),
        new Vector3 (-6.37f, -2.4f, 0)
    };

    // Goomba's movement
    public float goombaPatrolTime;
    public float goombaMaxOffset;
}
