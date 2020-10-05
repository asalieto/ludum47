using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed = 5;
    public bool teleportAtStart = false;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (teleportAtStart)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        Vector2 auxPos = new Vector2(0, 0);
        auxPos.x = Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime * speed);
        auxPos.y = Mathf.Lerp(transform.position.y, player.transform.position.y, Time.deltaTime * speed);
        transform.position = new Vector3(auxPos.x, auxPos.y, transform.position.z);
    }
}
