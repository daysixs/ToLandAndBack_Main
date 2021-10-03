using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    struct Raycast
    {
        public Vector2 topLeft, topRight, bottomLeft, bottomRight;
    }

    BoxCollider2D boxCollider;
    Raycast raycast;
    public LayerMask collisionMask;

    int horizontalRayCount = 3;
    int verticalRayCount = 3;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        CalcRaySpacing();
    }

    void SetRaycast()
    {
        //get the boundaries of player's collider
        Bounds bound = boxCollider.bounds;

        raycast.bottomLeft = new Vector2(bound.min.x, bound.min.y);
        raycast.bottomRight = new Vector2(bound.max.x, bound.min.y);
        raycast.topLeft = new Vector2(bound.min.x, bound.max.y);
        raycast.topRight = new Vector2(bound.max.x, bound.max.y);
    }

    void CalcRaySpacing()
    {
        Bounds bound = boxCollider.bounds;

        //minimum 2 so there's raycast in each corner of collission box
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        //To space out raycast
        horizontalRaySpacing = bound.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bound.size.x / (verticalRayCount - 1);
    }

    public void Movement(Vector3 vel)
    {
        SetRaycast();

        VerticalCollision(ref vel);

        transform.Translate(vel);
    }

    void VerticalCollision(ref Vector3 vel)
    {
        float dirY = Mathf.Sign(vel.y);
        float rayLength = Mathf.Abs(vel.y);

        for (int i = 0; i < verticalRayCount; i++)
        {
            //if moving down
            Vector2 rayOrigin = (dirY == -1) ? raycast.bottomLeft : raycast.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + vel.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * dirY, rayLength, collisionMask);

            Debug.DrawRay(raycast.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.down, Color.red);

            if (hit)
            {
                vel.y = hit.distance * dirY;
                rayLength = hit.distance;
            }
        }
    }

}
