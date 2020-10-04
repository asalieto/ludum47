using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItems : MonoBehaviour
{

    GameObject heldItem = null;
    GameObject overlappingItem = null;
    Animator playerAnim;
    KeyCode grabItemKey = KeyCode.E;
    [SerializeField]
    float grabOffset = 0.1f;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {

        if(overlappingItem != null && heldItem == null)
        {
            if (Input.GetKeyDown(grabItemKey))
            {
                heldItem = overlappingItem;
                heldItem.GetComponent<Collider2D>().enabled = false;
                Debug.Log("GRAB");
                AudioManager.Instance.PlaySFX(AudioManager.SFXType.Pickup, true);

                heldItem.transform.SetParent(transform);
                playerAnim.SetTrigger("Grab");
            }
        }
        else if (heldItem != null)
        {
            heldItem.transform.position = new Vector3(transform.position.x, transform.position.y + grabOffset, transform.position.z);

            if (Input.GetKeyDown(grabItemKey))
            {
                switch (playerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name)
                {
                    case "Player_CarryDown":
                    case "Player_IdleCarryDown":
                        heldItem.transform.position = new Vector3(transform.position.x, transform.position.y - grabOffset, transform.position.z);
                        break;
                    case "Player_CarryLeft":
                    case "Player_IdleCarryLeft":
                        heldItem.transform.position = new Vector3(transform.position.x - grabOffset, transform.position.y, transform.position.z);
                        break;
                    case "Player_CarryRight":
                    case "Player_IdleCarryRight":
                        heldItem.transform.position = new Vector3(transform.position.x + grabOffset, transform.position.y, transform.position.z);
                        break;
                    default:
                        break;
                }

                var currentRoomTransform = GameManager.Instance.CurrentPortal.GetCurrentRoomGO().transform;
                heldItem.transform.SetParent(currentRoomTransform);

                heldItem.GetComponent<Collider2D>().enabled = true;
                heldItem = null;
                playerAnim.SetTrigger("Grab");

                Debug.Log("RELEASE");

                AudioManager.Instance.PlaySFX(AudioManager.SFXType.Cat, true);
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
