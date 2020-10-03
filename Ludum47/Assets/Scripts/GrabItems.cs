using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItems : MonoBehaviour
{

    GameObject heldItem = null;
    GameObject overlappingItem = null;
    KeyCode grabItemKey = KeyCode.E;

    void Update()
    {
        if(overlappingItem != null && heldItem == null)
        {
            if (Input.GetKeyDown(grabItemKey))
            {
                heldItem = overlappingItem;
                heldItem.GetComponent<Collider2D>().enabled = false;
                Debug.Log("GRAB");

                heldItem.transform.SetParent(this.transform);
            }
        }
        else if (heldItem != null)
        {
            heldItem.transform.position = transform.position;
            if (Input.GetKeyDown(grabItemKey))
            {
                var currentRoomTransform = GameManager.Instance.CurrentPortal.GetCurrentRoomGO().transform;
                heldItem.transform.SetParent(currentRoomTransform);

                heldItem.GetComponent<Collider2D>().enabled = true;
                heldItem = null;
                Debug.Log("RELEASE");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ObjectGrab")
        {
            overlappingItem = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == overlappingItem)
        {
            overlappingItem = null;
        }
    }
}
