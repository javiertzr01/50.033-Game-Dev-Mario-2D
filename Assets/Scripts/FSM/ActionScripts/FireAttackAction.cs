using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/FireAttack")]
public class FireAttackAction : Action
{
    public int maxPrefabInScene = 3;
    public float impulseForce = 10;
    public float degree = 45;
    public GameObject attackPrefab;
    public BoolVariable marioFaceRight; // A scriptable object updated by PlayerMovement / PlayerController to store current Mario's facing

    public override void Act(StateController controller)
    {
        GameObject[] instantiatedPrefabsInScene = GameObject.FindGameObjectsWithTag(attackPrefab.tag);
        if (instantiatedPrefabsInScene.Length < maxPrefabInScene)
        {
            // Instantiate it where controller (Mario) is
            Debug.Log(controller.transform.parent.name);
            GameObject x = Instantiate(attackPrefab, controller.transform.parent.transform.position, Quaternion.identity);

            // Get the Rigidbody of the instantiated object
            Rigidbody2D rb = x.GetComponent<Rigidbody2D>();

            // Check if the Rigidbody component exists
            if (rb != null)
            {
                // Computer Direction Vector
                Vector2 direction = CalculateDirection(degree, marioFaceRight.value);

                // Apply a rightward impulse force to the object
                rb.AddForce(direction * impulseForce, ForceMode2D.Impulse);
            }
        }
    }

    public Vector2 CalculateDirection(float degrees, bool isFacingRight)
    {
        // Convert degrees to radians
        float radians = degrees * Mathf.Deg2Rad;

        // Calculate the Direction Vector
        float x = Mathf.Cos(radians);
        float y = Mathf.Sin(radians);

        // If the object is facing left, invert the x-component of the direction
        if (!isFacingRight)
        {
            x = -x;
        }

        return new Vector2(x, y);
    }
}
