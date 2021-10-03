using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals ("Player"))
        {
            Invoke("DropPlatform", 0.5f);
            Destroy(gameObject, 0.8f);
        }
    }

    void DropPlatform()
    {
        rb.isKinematic = false;
    }

}
