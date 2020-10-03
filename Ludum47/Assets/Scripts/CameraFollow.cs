using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject player;
    public float speed = 5;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        Vector2 auxPos = new Vector2(0,0);
        auxPos.x = Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime*speed);
        auxPos.y = Mathf.Lerp(transform.position.y, player.transform.position.y, Time.deltaTime*speed);
        transform.position = new Vector3(auxPos.x, auxPos.y, transform.position.z);
    }
}
