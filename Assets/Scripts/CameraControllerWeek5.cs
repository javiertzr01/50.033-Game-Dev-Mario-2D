using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerWeek5 : MonoBehaviour
{
    private Transform player;    // Mario's Transform
    public Transform endLimit;  // GameObject that indicated end of map
    private Vector3 startPosition;
    private float offset;   // initial x-offset between camera and Mario
    private float startX;   // smallest x-coordinate of the camera
    private float endX;   // largest x-coordinate of the camera
    private float viewportHalfwidth;    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        startPosition = transform.position;

        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        viewportHalfwidth = Mathf.Abs(bottomLeft.x - this.transform.position.x);
        offset = this.transform.position.x - player.position.x;
        startX = this.transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfwidth;
    }

    // Update is called once per frame
    void Update()
    {
        float desiredX = player.position.x;
        if (desiredX > startX && desiredX < endX)
        {
            this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
        }
    }

    public void GameRestart()
    {
        transform.position = startPosition;
    }
}
