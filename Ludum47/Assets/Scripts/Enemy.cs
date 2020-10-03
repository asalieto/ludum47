using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Action<int> OnDie;
    public bool TEST_Die = false;

    public int EnemyID;

    private Vector2 m_originPos;
    private bool m_alive;

    // Start is called before the first frame update
    void Start()
    {
        m_originPos = transform.position;
    }

    private void Update()
    {
        if(TEST_Die)
        {
            Die();
            enabled = false;
        }
    }

    private void Die()
    {
        m_alive = false;
        OnDie(EnemyID);
    }

    public bool IsAlive()
    {
        return m_alive;
    }

    public void ResetEnemy()
    {
        TEST_Die = false;
        enabled = true;
        m_alive = true;
        transform.position = m_originPos;

    }
}
