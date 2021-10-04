using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float moveSpeed, jumpSpeed, xOffset, yOffset, xSize, ySize;
    public float jumpForce;
    public bool isGrounded;
    public LayerMask groundCheck;
    private Rigidbody2D rb;
    
    private void Start()
    {  
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * moveSpeed;

        isGrounded = Physics2D.OverlapBox(new Vector2(transform.position.x + xOffset, transform.position.y + yOffset), new Vector2(xSize, ySize), 0f, groundCheck);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

        playerVar.isJumping = !isGrounded;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector2(transform.position.x + xOffset, transform.position.y + yOffset), new Vector2(xSize, ySize));
    }
}
