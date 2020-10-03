using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    public float speed = 5f;
    HealthManager playerHM;
    Animator PlayerAnim;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerHM = GetComponent<HealthManager>();
        PlayerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 auxVel = new Vector2(0.0f, 0.0f);

        if (playerHM.isAlive())
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                auxVel.y += speed;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                auxVel.y -= speed;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                auxVel.x += speed;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                auxVel.x -= speed;
            }
        }

        playerRB.velocity = auxVel;
        PlayerAnim.SetFloat("SpeedX", auxVel.x);
        PlayerAnim.SetFloat("SpeedY", auxVel.y);

        /*transform.position.Set(transform.position.x + auxVel.x * Time.deltaTime,
                               transform.position.y + auxVel.y * Time.deltaTime,
                               transform.position.z);*/

        if(playerRB.velocity.magnitude != 0f)
        {
            m_lastVelocityDirection = playerRB.velocity.normalized;
            m_lastVelocityDirection.Normalize();

            angle = Mathf.Atan2(playerRB.velocity.y, playerRB.velocity.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Projectile);

        var bullet = Instantiate(m_bulletPrefab, transform.position + new Vector3(m_lastVelocityDirection.x, m_lastVelocityDirection.y, 0) * m_bulletSeparationMultiplier, Quaternion.AngleAxis(angle, Vector3.forward));
        bullet.GetComponent<Bullet>().Init(m_lastVelocityDirection.normalized);
    }

    private Vector2 m_lastVelocityDirection = Vector2.down;
    private float angle = 0;
    private float m_currentBulletInterval = 0f;

    [SerializeField]
    private GameObject m_bulletPrefab = null;
    [SerializeField]
    private float m_bulletInterval = 0.2f;
    [SerializeField]
    private float m_bulletSeparationMultiplier = 0.02f;
}
