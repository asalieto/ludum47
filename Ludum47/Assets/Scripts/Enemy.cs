using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool Alive;

    void Start()
    {
        m_destinationIndex = 0;
        m_foward = true;
    }

    public bool IsAlive()
    {
        return Alive;
    }

    private void Update()
    {
        if (Alive)
        {
            Move();

            if (Vector3.Distance(this.transform.position, m_waypoints[m_destinationIndex].position) <= m_destinationOffset)
            {
                GoToNextWaypoint();
            }
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

    private float m_waypointRadius = 0.15f;
    private int m_destinationIndex = 0;
    private bool m_foward;

    [SerializeField]
    private float m_speed = 1.5f;
    [SerializeField]
    private float m_destinationOffset = 0.05f;
    [SerializeField]
    private bool m_circular;
    [SerializeField]
    private List<Transform> m_waypoints = null;
}
