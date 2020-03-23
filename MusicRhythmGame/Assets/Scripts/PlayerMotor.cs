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
    private float animationDuration = 2.5f;
    private float firstLane = -5.0f;
    private float secondLane = -1.66f;
    private float thirdLane = 1.66f;
    private float forthLane = 5.0f;
    private float target = 0.0f;
    private float startTime;
    private float timer;
    private int leftOrRight = 0;
    private bool isDead = false;
    private bool attacking = false;

    public bool Attacking { get => attacking; set => attacking = value; }


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
        } else {
            Movement();
        }
    }

    public void SetSpeed(float modifier) {
        speed = 5.0f + modifier;
    }

    // it is called when the character hits something
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.tag == "Enemy") {
            // attack minions when the character hits them
            Animator minAnim = hit.gameObject.GetComponent<Animator>();
            hit.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            if (Attacking)
            {
                minAnim.SetBool("isDead", true);
                GetComponent<Score>().IncreaseScore();
            }
            else
            {
                minAnim.SetBool("isAttack", true);
            }
        }
    }

    private void Movement(){
        if (timer - startTime < animationDuration)
        {
            Debug.Log("time:"+ timer);
        }
        moveVector = Vector3.zero;

        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f){
            //Debug.Log("End Charge "+ ++tempCount + " "+ animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            animator.SetInteger("condition", 0);
            Attacking = false;
        }
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f);
        // recalculate the moveVector
        if (Input.GetButtonDown("First Lane"))
            target = firstLane;
        else if (Input.GetButtonDown("Second Lane"))
            target = secondLane;
        else if (Input.GetButtonDown("Third Lane"))
            target = thirdLane;
        else if (Input.GetButtonDown("Forth Lane"))
            target = forthLane;

        if (Input.GetButtonDown("First Lane") || Input.GetButtonDown("Second Lane") || Input.GetButtonDown("Third Lane") || Input.GetButtonDown("Forth Lane"))
            Attack();
        
        // determine the Waarrior is on the Lane or not
        if (transform.position.x > target - 0.2f && transform.position.x < target + 0.2f)
        {
            if (transform.position.x > target - 0.1f && transform.position.x < target + 0.1f)
                leftOrRight = 0;
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
        animator.Play("Attack", -1, 0f);
        Attacking = true;
    }

    public void Dead() {
        isDead = true;
        GetComponent<Score>().OnDeath();
        Debug.Log("Death");
    }

}
