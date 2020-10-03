using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public int maxHealth = 3;
    int currentHealth;
    bool alive = true;
    Vector3 initialPosition;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            alive = false;
        }
    }

    void Respawn()
    {
        transform.position = initialPosition;
        alive = true;
        currentHealth = maxHealth;
    }

    public bool isAlive()
    {
        return alive;
    }

    public void receiveDamage(int damage)
    {
        currentHealth -= damage;
    }
}
