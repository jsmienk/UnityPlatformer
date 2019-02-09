using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed;
    public bool getOffsetAuto;
    public bool inverseY;
    public float pitchMin = -60f;
    public float pitchMax = 60f;

    public Transform target;
    public Transform pivot;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // Hide cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Get offset from scene if enabled
        if (getOffsetAuto)
        {
            offset = target.position - this.transform.position;
        }

        // Uncouple from camera
        pivot.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        // Keep pivot at target position
        pivot.position = target.position;
        pivot.Rotate(0f, Input.GetAxis("Mouse X") * rotateSpeed, 0f);
        pivot.Rotate((inverseY ? 1f : -1f) * Input.GetAxis("Mouse Y") * rotateSpeed, 0f, 0f);

        // Limit pitch
        float pitch = pivot.eulerAngles.x;
        if (pitch > pitchMax && pitch < 180f)
        {
            pivot.rotation = Quaternion.Euler(pitchMax, 0f, 0f);
        }
        if (pitch < 360f + pitchMin && pitch > 180f)
        {
            pivot.rotation = Quaternion.Euler(360f + pitchMin, 0f, 0f);
        }
        
        // Move camera based on target rotation and offset
        Quaternion rotation = Quaternion.Euler(pivot.eulerAngles.x, pivot.eulerAngles.y, 0f);
        this.transform.position = target.position - (rotation * offset);

        // Limit y axis to that of the target
        if (this.transform.position.y < target.position.y)
        {
            this.transform.position = new Vector3(
                this.transform.position.x,
                target.position.y - .5f,
                this.transform.position.z
            );
        }

        this.transform.LookAt(target);
    }
}
