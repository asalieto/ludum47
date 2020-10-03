using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool Alive;

    void Start()
    {
        _destinationIndex = 0;
        _foward = true;
    }

    public bool IsAlive()
    {
        return Alive;
    }

    private void Update()
    {
        if (Alive && _waypoints != null && _waypoints.Count > 0)
        {
            Move();

            if (Vector3.Distance(this.transform.position, _waypoints[_destinationIndex].position) <= _destinationOffset)
            {
                GoToNextWaypoint();
            }
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_destinationIndex].position, _speed * Time.deltaTime);
    }

    private void GoToNextWaypoint()
    {
        if (_circular)
        {
            _destinationIndex = (_destinationIndex + 1) % _waypoints.Count;
        }
        else
        {
            if (_foward && _destinationIndex + 1 >= _waypoints.Count)
            {
                _foward = false;
            }
            else if (_destinationIndex - 1 < 0)
            {
                _foward = true;
            }

            _destinationIndex = _foward ? _destinationIndex + 1 : _destinationIndex - 1;
        }
    }

    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (_waypoints != null)
        {
            for (int i = 0; i < _waypoints.Count; i++)
            {
                if (_waypoints[i] != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(_waypoints[i].position, _waypointRadius);

                    int nextWaypoint = i + 1;

                    if (_circular)
                    {
                        nextWaypoint = nextWaypoint % _waypoints.Count;
                    }

                    if (nextWaypoint < _waypoints.Count && _waypoints[nextWaypoint] != null)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(_waypoints[i].position, _waypoints[nextWaypoint].position);
                    }
                }
            }
        }
#endif
    }

    private float _waypointRadius = 0.15f;
    private int _destinationIndex = 0;
    private bool _foward;

    [SerializeField]
    private float _speed = 1.5f;
    [SerializeField]
    private float _destinationOffset = 0.05f;
    [SerializeField]
    private bool _circular;
    [SerializeField]
    private List<Transform> _waypoints = null;
}
