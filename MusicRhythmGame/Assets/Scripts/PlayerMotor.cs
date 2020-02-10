﻿using System.Collections;
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
    private float startTime;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        controller = GetComponent<CharacterController> ();
        animator = GetComponent<Animator> ();
        // startTime = Time.time;
        startTime = 0f;
        timer = 0f;
        animator.SetInteger("condition", 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (isDead) {
            return;
        }

        // Disable user controller at the beginning of 2 seconds
        if (timer - startTime < animationDuration) {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        Movement();
    }

    public void SetSpeed(float modifier) {
        speed = 5.0f + modifier;
    }

    // it is called when the character hits something
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.point.z > transform.position.z + 0.1f && hit.gameObject.tag == "Enemy") {
            // attack minions when the character hits them
            Destroy(hit.gameObject);
            GetComponent<Score>().IncreaseScore();
        }
    }

    private void Movement(){
        moveVector = Vector3.zero;

        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f){
            //Debug.Log("End Charge "+ ++tempCount + " "+ animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            animator.SetInteger("condition", 0);
        }
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f);
        // recalculate the moveVector
        if (Input.GetButtonDown("First Lane"))
            target = firstLane;
        if (Input.GetButtonDown("Second Lane"))
            target = secondLane;
        if (Input.GetButtonDown("Third Lane"))
            target = thirdLane;
        if (Input.GetButtonDown("Forth Lane"))
            target = forthLane;
        // determine the Waarrior is on the Lane or not
        if (transform.position.x > target - 0.1f && transform.position.x < target + 0.1f)
        {
            leftOrRight = 0;
            if(Input.GetButtonDown("First Lane") || Input.GetButtonDown("Second Lane") || Input.GetButtonDown("Third Lane") || Input.GetButtonDown("Forth Lane"))
                Attack();
        }
        else
            leftOrRight = 1;
        // X - Left and Right
        moveVector.x = leftOrRight * (target - transform.position.x) / 0.15f;
        // Y - Up and Down
        moveVector.y = verticalVelocity;
        // Z - Forward and Backward
        moveVector.z = speed;

        // move 5 meters per second
        controller.Move(moveVector * Time.deltaTime);
    }
    private void Attack() {
        if (animator.GetInteger("condition") == 0)
            animator.SetInteger("condition", 1);
        else
            animator.Play("Attack", -1, 0f);
    }

    public void Dead() {
        isDead = true;
        GetComponent<Score>().OnDeath();
        Debug.Log("Death");
    }

}
