using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Controller))] //automatically adds the "Controller" script 
public class PlayerMovement : MonoBehaviour
{
    Vector3 velocity;
    bool isClimbing = false;
    public float initialGravity;
    float gravity;

    [Header("Movement stats")]
    public float walkSpeed = 6f;
    public float climbSpeed = 4f;
    float accelerationTimeInAir = .2f;
    float accelerationTimeInGround = .1f;

    [Header("Jump")]
    public float jumpVelocity;
    public float jumpHeight = 4f;
    public float timeToJump = .4f;

    float velocityXSmoothing;

    Controller controller;

    void Start()
    {
        controller = GetComponent<Controller>();
        initialGravity = -(2 * jumpHeight) / Mathf.Pow(timeToJump, 2);
        gravity = initialGravity;
        jumpVelocity = Mathf.Abs(gravity) * timeToJump;
    }

    void FixedUpdate()
    {
        controller.Movement(velocity * Time.deltaTime);

        //prevents gravity from building up
        if (controller.collisions.top || controller.collisions.bottom)
            velocity.y = 0;
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //Storing input key

        float velocityX = input.x * walkSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, velocityX, ref velocityXSmoothing, (controller.collisions.top) ? accelerationTimeInGround : accelerationTimeInAir); //vertical movement, slow down smoothly when stopped moving
        velocity.y += gravity * Time.deltaTime; //gravity

        //jump
        if (Input.GetKey(KeyCode.Z))
        {
            if (controller.collisions.bottom)
                velocity.y = jumpVelocity;
        }

        ClimbLadder();

    }

    void ClimbLadder()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //climb
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (controller.collisions.climbingLadder)
            {
                isClimbing = true;
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                gravity = 0;
                velocity.x = 0;
                velocity.y = 0;
            }
        }

        if (isClimbing)
        {
            if (controller.collisions.climbingLadder)
                velocity.y = input.y * climbSpeed;
            else
                velocity.y = 0;

            //Cancel climbing
            if (Input.GetKeyDown(KeyCode.C) || Mathf.Abs(input.x) == 1)
            {
                isClimbing = false;
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                gravity = initialGravity;
            }

            //Jump while climbing
            if (Input.GetKey(KeyCode.Z))
            {
                isClimbing = false;
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                gravity = initialGravity;
                velocity.y = jumpVelocity;
            }
        }

    }
}
