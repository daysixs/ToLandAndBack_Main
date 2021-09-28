using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Controller))] //automatically adds the "Controller" script 
public class Player : MonoBehaviour
{
    Vector3 velocity;
    float gravity;

    [Header("Movement stats")]
    public float speed = 6f;
    float accelerationTimeInAir = .2f;
    float accelerationTimeInGround = .1f;

    [Header("Jump")]
    public float jumpVelocity;
    public float jumpHeight = 4f;
    public float timeToJump = .4f;
    public bool doubleJump;

    float velocityXSmoothing;

    Controller controller;

    void Start()
    {
        controller = GetComponent<Controller>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJump, 2); 
        jumpVelocity = Mathf.Abs(gravity) * timeToJump;
    }

    void FixedUpdate()
    {
        controller.Move(velocity * Time.deltaTime);

        //prevents gravity from building up
        if (controller.collisions.above || controller.collisions.below)
            velocity.y = 0;
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //Storing input key
    
        float velocityX = input.x * speed;
        velocity.x = Mathf.SmoothDamp(velocity.x, velocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeInGround : accelerationTimeInAir); //vertical movement, slow down smoothly when stopped moving
        velocity.y += gravity * Time.deltaTime; //gravity

        if (Input.GetKey(KeyCode.Z))
        {
            if (controller.collisions.below)
                velocity.y = jumpVelocity;
        }
    }
}
