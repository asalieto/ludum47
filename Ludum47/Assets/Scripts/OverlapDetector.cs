using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapDetector : MonoBehaviour
{

    bool active = false;
    int overlappingElements = 0;
    public int id = 0;

    public bool IsActive()
    {
        return active;
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.tag == "ObjectGrab")
        {
            if(collision.gameObject.GetComponent<ObjectID>().id == id)
            {
                overlappingElements++;
                if (overlappingElements > 0)
                {
                    active = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.tag == "ObjectGrab")
        {
            if (collision.gameObject.GetComponent<ObjectID>().id == id)
            {
                overlappingElements--;
                if (overlappingElements <= 0)
                {
                    active = false;
                }
            }
        }
    }
}
