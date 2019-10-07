using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    private float speed = 5.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    private float animationDuration = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController> ();
    }

    // Update is called once per frame
    void Update()
    {
        // Disable user controller at the beginning of 2 seconds
        if (Time.time < animationDuration) {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        // reset moveVector in every single frame
        moveVector = Vector3.zero;
        
        if (controller.isGrounded) {
            verticalVelocity = -0.5f;
        } else {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // recalculate the moveVector
        // X - Left and Right
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        // Y - Up and Down
        moveVector.y = verticalVelocity;
        // Z - Forward and Backward
        moveVector.z = speed;

        // move 5 meters per second
        controller.Move(moveVector * Time.deltaTime);
    }
}
