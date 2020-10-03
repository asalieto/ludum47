using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D playerRB;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 auxVel = new Vector2(0.0f, 0.0f);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            auxVel.y += speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            auxVel.y -= speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            auxVel.x += speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            auxVel.x -= speed;
        }

        playerRB.velocity = auxVel;
        /*transform.position.Set(transform.position.x + auxVel.x * Time.deltaTime,
                               transform.position.y + auxVel.y * Time.deltaTime,
                               transform.position.z);*/
    }
}
