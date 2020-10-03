﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapDetector : MonoBehaviour
{

    bool active = false;
    int overlappingElements = 0;
    public int id = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            Debug.Log("ACTIVE");
        }
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
