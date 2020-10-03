using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItems : MonoBehaviour
{

    GameObject heldItem = null;
    GameObject overlappingItem = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(overlappingItem != null && heldItem == null)
        {
            Debug.Log("IN TRIGGER");
            if (Input.GetKeyDown(KeyCode.E))
            {
                heldItem = overlappingItem;
                heldItem.GetComponent<Collider2D>().enabled = false;
                Debug.Log("GRAB");
            }
        }
        else if (heldItem != null)
        {
            heldItem.transform.position = transform.position;
            if (Input.GetKeyDown(KeyCode.E))
            {
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
