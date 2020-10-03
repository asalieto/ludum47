using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    public float speed = 5f;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

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

        if(playerRB.velocity.magnitude != 0f)
        {
            m_lastVelocityDirection = playerRB.velocity;

            float angle = Mathf.Atan2(playerRB.velocity.y, playerRB.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        m_currentBulletInterval += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            if(m_currentBulletInterval > m_bulletInterval)
            {
                Shoot();
                m_currentBulletInterval = 0f;
            }
        }
    }

    private void Shoot()
    {
        var bullet = GameObject.Instantiate(m_bulletPrefab, this.transform.position + (transform.up * m_bulletSeparationMultiplier), Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(m_lastVelocityDirection.normalized);
    }

    private Vector2 m_lastVelocityDirection = Vector2.up;
    private float m_currentBulletInterval = 0f;

    [SerializeField]
    private GameObject m_bulletPrefab = null;
    [SerializeField]
    private float m_bulletInterval = 0.2f;
    [SerializeField]
    private float m_bulletSeparationMultiplier = 0.02f;
}
