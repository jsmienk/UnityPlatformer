using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveOffset;
    public float duration;

    private Vector3 startPos;
    private float time;
    private bool isGoingForward;

    // Start is called before the first frame update
    void Start()
    {
        this.startPos = this.transform.position;
        this.isGoingForward = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Increase / decrease duration
        this.time = this.time + (this.isGoingForward ? 1f : -1f) * Time.deltaTime;

        // Set position
        this.transform.position = this.startPos + (this.moveOffset * this.time / this.duration);
        
        // Toggle
        if (this.time <= 0 || this.time >= this.duration)
        {
            this.isGoingForward = !this.isGoingForward;
        }
    }
}
