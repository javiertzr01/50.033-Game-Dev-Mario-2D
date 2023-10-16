using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    // lives
    public int maxLives;

    // Mario
    public int speed;
    public int maxSpeed;
    public int upSpeed;
    public int deathImpulse;
    public Vector3[] spawnLocations = {
        new Vector3 (-8.26f, -4.22f, 0),
        new Vector3 (-6.37f, -2.4f, 0)
    };
    public float flickerInterval;

    // Goomba
    public Dictionary<string, Vector3[]> goombaSpawnLocations = new Dictionary<string, Vector3[]> 
    {

        { "World 1-1", new Vector3[] { new Vector3 (9.0f, -4.47f, 0.0f), new Vector3 (28.0f, -4.47f, 0.0f), new Vector3 (39.0f, -4.47f, 0.0f), new Vector3 (40.8f, -4.47f, 0.0f) }},
        { "World 1-2", new Vector3[] { new Vector3 (0.69f, -2.46f, 0.0f)}}
    };
    public float goombaPatrolTime;
    public float goombaMaxOffset;
}
