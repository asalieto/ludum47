﻿using UnityEngine;

public class FireTrap : MonoBehaviour
{
    void Start()
    {
        m_currentInterval = 0;
        m_anim = GetComponent<Animator>();

        if(m_direction != Direction.Down)
        {
            m_anim.SetBool("FacingUp", true);
        }
    }

    void Update()
    {
        m_currentInterval += Time.deltaTime;

        if (m_currentInterval > m_interval)
        {
            Shoot();
            m_currentInterval = 0f;
        }
    }

    private void Shoot()
    {
        AudioManager.Instance.PlaySFX("trap", false);

        Vector2 vector = GetDirectionVector();
        float angle = Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
        
        if(m_direction == Direction.Up || m_direction == Direction.Down)
        {
            angle = angle + 90;
        }
        else
        {
            angle = angle - 90;
        }

        var bullet = Instantiate(m_bulletPrefab, transform.position + new Vector3(vector.x, vector.y, 0) * m_bulletSeparationMultiplier, Quaternion.AngleAxis(angle, Vector3.forward));
        bullet.GetComponent<Bullet>().Init(vector.normalized);

        m_anim.SetTrigger("Shoot");
    }

    private Animator m_anim;
    private float m_currentInterval = 0f;

    [SerializeField]
    private float m_interval = 1.5f;
    [SerializeField]
    private Direction m_direction = Direction.Right;
    [SerializeField]
    private GameObject m_bulletPrefab = null;
    [SerializeField]
    private float m_bulletSeparationMultiplier = 0.02f;

    private Vector2 GetDirectionVector()
    {
        switch (m_direction)
        {
            case Direction.Up:
                return Vector2.up;
            case Direction.Right:
                return Vector2.right;
            case Direction.Down:
                return Vector2.down;
            case Direction.Left:
                return Vector2.left;
        }

        return Vector2.zero;
    }

    enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
}
