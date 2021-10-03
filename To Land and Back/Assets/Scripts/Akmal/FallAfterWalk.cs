using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAfterWalk : MonoBehaviour
{
    //not sure yet havent test it out lols
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.FindWithTag("Ceilling").GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
