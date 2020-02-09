using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Vector3 moveVector;
    private float speed = 5.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    private float animationDuration = 2.0f;
    private float firstLane = -5.0f;
    private float secondLane = -1.66f;
    private float thirdLane = 1.66f;
    private float forthLane = 5.0f;
    private float target = 0.0f;

    private int leftOrRight = 0;


    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController> ();
        animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) {
            return;
        }

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
        if (Input.GetButtonDown("First Lane"))
        {
            if (transform.position.x != firstLane)
                leftOrRight = 1;
            target = firstLane;
            //Debug.Log("First Lane " + moveVector.x);
        }
        if (Input.GetButtonDown("Second Lane"))
        {
            if (transform.position.x != secondLane)
                leftOrRight = 1;
            target = secondLane;
            //Debug.Log("Second Lane "+ moveVector.x);
        }
        if (Input.GetButtonDown("Third Lane"))
        {
            if (transform.position.x != thirdLane)
                leftOrRight = 1;
            target = thirdLane;      
            //Debug.Log("Third Lane "+ moveVector.x);
        }
        if (Input.GetButtonDown("Forth Lane"))
        {
            if (transform.position.x != forthLane)
                leftOrRight = 1;
            target = forthLane;
            //Debug.Log("Forth Lane "+ moveVector.x);
        }
        // X - Left and Right
        if(transform.position.x > target-0.1f && transform.position.x < target+0.1f)
            leftOrRight = 0;
        moveVector.x = leftOrRight * (target - transform.position.x) / 0.25f;
        // Y - Up and Down
        moveVector.y = verticalVelocity;
        // Z - Forward and Backward
        moveVector.z = speed;

        // move 5 meters per second
        controller.Move(moveVector * Time.deltaTime);
    }

    public void SetSpeed(float modifier) {
        speed = 5.0f + modifier;
    }

    // it is called when the character hits something
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.point.z > transform.position.z + controller.radius) {
            // attack minions when the character hits them
            Dead();
        }
    }

    private void Attack() {
        Debug.Log("Attack!");
    }

    private void Dead() {
        isDead = true;
        GetComponent<Score>().OnDeath();
        Debug.Log("Dealth");
    }
}
