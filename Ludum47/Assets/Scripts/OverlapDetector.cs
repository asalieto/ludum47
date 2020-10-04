using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapDetector : MonoBehaviour
{

    bool active = false;
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
                active = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!gameObject.activeInHierarchy)
        {
            return;
        }

        if (!collision.isTrigger && collision.gameObject.tag == "ObjectGrab")
        {
            if (collision.gameObject.GetComponent<ObjectID>().id == id)
            {
                active = false;
            }
        }
    }
}
