using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Action<int> OnDie;
    public bool TEST_Die = false;

    public int EnemyID;

    [SerializeField] private float m_speed = 1.5f;
    [SerializeField] private float m_destinationOffset = 0.05f;
    [SerializeField] private bool m_circular;
    [SerializeField] private List<Transform> m_waypoints = null;

    private Vector2 m_originPos;
    private bool m_alive;

    private float m_waypointRadius = 0.15f;
    private int m_destinationIndex = 0;
    private bool m_foward;

    void Start()
    {
        m_destinationIndex = 0;
        m_foward = true;
        m_alive = true;

        m_originPos = transform.position;

        this.gameObject.SetActive(true);
    }

    public void Die()
    {
        m_alive = false;
        OnDie(EnemyID);

        this.gameObject.SetActive(false);
    }

    public bool IsAlive()
    {
        return m_alive;
    }

    public void ResetEnemy()
    {
        TEST_Die = false;
        m_alive = true;
        transform.position = m_originPos;

        this.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (m_alive && m_waypoints != null && m_waypoints.Count > 0)
        {
            Move();

            if (Vector3.Distance(this.transform.position, m_waypoints[m_destinationIndex].position) <= m_destinationOffset)
            {
                GoToNextWaypoint();
            }
        }

        if (TEST_Die)
        {
            Die();
        }
    }

    private void Move()
    {
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
}
