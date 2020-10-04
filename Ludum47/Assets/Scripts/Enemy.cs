using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Action<int> OnDie;

    public int EnemyID;

    [SerializeField] private float m_speed = 1.5f;
    [SerializeField] private float m_destinationOffset = 0.05f;
    [SerializeField] private float m_distanceSeePlayer = 1.5f;
    [SerializeField] private float m_TimeBetweenShoot = 2f;
    [SerializeField] private bool  m_circular;
    [SerializeField] private List<Transform> m_waypoints = null;
    [SerializeField] private float m_bulletSeparationMultiplier = 0.02f;
    [SerializeField] private GameObject m_bulletPrefab = null;

    private GameObject m_player;
    private Vector2 m_originPos;
    private bool m_alive;
    private Animator m_anim;

    private float m_waypointRadius = 0.15f;
    private int m_destinationIndex = 0;
    private bool m_foward;
    private bool m_canShoot = true;
    private bool m_isShooting = false;

    void Start()
    {
        m_destinationIndex = 0;
        m_foward = true;
        m_alive = true;

        if (m_waypoints != null && m_waypoints.Count > 0)
        {
            transform.position = m_waypoints[m_destinationIndex].position;
        }

        m_originPos = transform.position;

        m_player = GameObject.FindGameObjectWithTag("Player");
        m_anim = GetComponent<Animator>();
        this.gameObject.SetActive(true);
    }

    public void Die()
    {
        m_alive = false;
        m_isShooting = false;
        OnDie?.Invoke(EnemyID);

        m_anim.SetTrigger("Dead");
        GetComponent<BoxCollider2D>().enabled = false;

        //this.gameObject.SetActive(false);
    }

    public bool IsAlive()
    {
        return m_alive;
    }

    public void ResetEnemy()
    {
        if (!m_alive)
        {
            m_anim.SetTrigger("Respawn");
        }

        m_canShoot = true;
        m_alive = true;
        m_isShooting = false;
        transform.position = m_originPos;
        m_destinationIndex = 0;

        GetComponent<HealthManager>().Respawn();

        this.gameObject.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void Update()
    {

        Debug.Log(m_anim.speed);

        if (m_alive && m_waypoints != null && m_waypoints.Count > 0 && !m_isShooting)
        {
            Move();

            if (Vector3.Distance(this.transform.position, m_waypoints[m_destinationIndex].position) <= m_destinationOffset)
            {
                GoToNextWaypoint();
            }
        }

        if(m_alive)
        {
            TryShootEnemy();
        }
    }

    private void Move()
    {
        m_anim.SetFloat("SpeedX", (m_waypoints[m_destinationIndex].position.x - transform.position.x));
        m_anim.SetFloat("SpeedY", (m_waypoints[m_destinationIndex].position.y - transform.position.y));

        transform.position = Vector2.MoveTowards(transform.position, m_waypoints[m_destinationIndex].position, m_speed * Time.deltaTime);
    }

    private void GoToNextWaypoint()
    {
        if (m_circular)
        {
            m_destinationIndex = (m_destinationIndex + 1) % m_waypoints.Count;
        }
        else
        {
            if (m_foward && m_destinationIndex + 1 >= m_waypoints.Count)
            {
                m_foward = false;
            }
            else if (m_destinationIndex - 1 < 0)
            {
                m_foward = true;
            }

            m_destinationIndex = m_foward ? m_destinationIndex + 1 : m_destinationIndex - 1;
        }
    }

    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (m_waypoints != null)
        {
            for (int i = 0; i < m_waypoints.Count; i++)
            {
                if (m_waypoints[i] != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(m_waypoints[i].position, m_waypointRadius);

                    int nextWaypoint = i + 1;

                    if (m_circular)
                    {
                        nextWaypoint = nextWaypoint % m_waypoints.Count;
                    }

                    if (nextWaypoint < m_waypoints.Count && m_waypoints[nextWaypoint] != null)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(m_waypoints[i].position, m_waypoints[nextWaypoint].position);
                    }
                }
            }
        }
#endif
    }

    private void TryShootEnemy()
    {
        if(m_canShoot)
        {
            if(CheckPlayerInRange())
            {
                m_canShoot = false;
                m_isShooting = true;
                Shoot();

                StartCoroutine("ShootDelay");
            }
            else
            {
                m_isShooting = false;
            }
        }
    }

    private bool CheckPlayerInRange()
    {
        if(Vector2.Distance(m_player.transform.position, transform.position ) < m_distanceSeePlayer)
        {
            int layerMask = LayerMask.GetMask("Enemy");
            layerMask = ~layerMask;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, m_player.transform.position - transform.position, m_distanceSeePlayer, layerMask);

            if(hit && hit.transform.tag == "Player")
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(m_TimeBetweenShoot);

        m_canShoot = true;
    }
    
    private void Shoot()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Projectile);

        GameObject bullet = GameObject.Instantiate(m_bulletPrefab, this.transform.position + (transform.up * m_bulletSeparationMultiplier),
            Quaternion.FromToRotation(Vector3.up, m_player.transform.position - transform.position));
        bullet.GetComponent<Bullet>().Init((m_player.transform.position - transform.position).normalized);

        m_anim.SetTrigger("Shoot");
    }
}
