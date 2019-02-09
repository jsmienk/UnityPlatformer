using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public float rotateSpeed;

    public CharacterController controller;
    public Transform pivot;
    public Animator animator;
    public GameObject model;

    private Vector3 moveSpeedVector;    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Remember current y axis because it must not be normalized
        float moveSpeedY = moveSpeedVector.y;
        // Use inputs to form new directions
        moveSpeedVector = this.transform.forward * Input.GetAxisRaw("Vertical") + 
                          this.transform.right * Input.GetAxisRaw("Horizontal");
        // Normalize to prevent going faster when moving diagonal
        moveSpeedVector = moveSpeedVector.normalized * moveSpeed;
        // Restore y axis value
        moveSpeedVector.y = moveSpeedY;

        // Check if player is on the ground
        if (controller.isGrounded)
        {
            // Prevent gravity from decreasing constantly
            moveSpeedVector.y = 0f;

            // Jump
            if (Input.GetButtonDown("Jump"))
            {
                moveSpeedVector.y = jumpForce;
            }
        }

        // Gravity
        moveSpeedVector.y = moveSpeedVector.y + Physics.gravity.y * gravityScale * Time.deltaTime;
        // Apply movement
        controller.Move(moveSpeedVector * Time.deltaTime);

        // If any direction is pressed
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            // Rotate player towards camera
            this.transform.rotation = Quaternion.Euler(0f, pivot.eulerAngles.y, 0f);
            
            // Smoothly rotate model towards player
            Vector3 desiredRotationVector = new Vector3(moveSpeedVector.x, 0f, moveSpeedVector.z);
            if (desiredRotationVector != Vector3.zero)
            {
                model.transform.rotation = Quaternion.Slerp(
                    model.transform.rotation, 
                    Quaternion.LookRotation(desiredRotationVector), 
                    rotateSpeed * Time.deltaTime
                );
            }
        }

        animator.SetBool("isGrounded", controller.isGrounded);
        animator.SetFloat("speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))) * moveSpeed);
    }
}
