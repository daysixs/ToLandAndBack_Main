using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    public enum InteractionType { NONE, PickUp, Inspect }
    public InteractionType type;

    public string descriptionText;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;  // box collider is a type of collider2d which will perm set isTrigger on
        gameObject.layer = 7;
    }

    public void Interact()
    {
        switch(type)
        {
            case InteractionType.PickUp:
                Debug.Log("Pick-up");
                // can be upgraded to singleton
                FindObjectOfType<InteractionSystem>().PickUpItem(gameObject); // adding object to pickupitem list
                gameObject.SetActive(false);
                break;
            case InteractionType.Inspect:
                // can be upgraded to singleton
                // call examine item
                FindObjectOfType<InteractionSystem>().ExamineItem(this);
                Debug.Log("Inspect");
                break;
            default:
                Debug.Log("Nothing there");
                break;
        }
    }
}
