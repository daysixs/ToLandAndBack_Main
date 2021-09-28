using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    // pick-up
    public Transform detectPoint;
    private const float detectRadius = 0.2f;
    public LayerMask detectCheck;
    bool isDetected;

    // cached trigger object
    public GameObject detectedObject;

    // examine
    public GameObject examineWindow;
    public Image examineImage;
    public Text examineText;

    // others
    public List<GameObject> pickedUpItems = new List<GameObject>();
 
    void Update()
    {
        if(ObjectDetect())
        {
            if(InteractInput())
            {
                detectedObject.GetComponent<Item>().Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectPoint.position, detectRadius);
    }

    bool InteractInput()
    {
       return Input.GetKeyDown(KeyCode.E);
    }

    bool ObjectDetect()
    {
        Collider2D obj = Physics2D.OverlapCircle(detectPoint.position, detectRadius, detectCheck);
        if (obj == null)
        {
            detectedObject = null;
            return false;
        }
        else
        {
            detectedObject = obj.gameObject;
            return true;
        }
    }

    public void PickUpItem(GameObject item)
    {
        pickedUpItems.Add(item);
    }

    public void ExamineItem(Item item)
    {
        // display examine window
        examineImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
        // show item image
        examineText.text = item.descriptionText;
        // write text
        examineWindow.SetActive(true);
    }

}
