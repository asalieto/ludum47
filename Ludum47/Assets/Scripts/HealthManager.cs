using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public int maxHealth = 3;
    public int currentHealth;
    bool alive = true;
    Vector3 initialPosition;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        initialPosition = transform.position;
    }

    public void Respawn()
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

        if (currentHealth <= 0)
        {
            alive = false;

            if(tag == "Player")
            {
                Debug.Log("Player Dead!");
                GameObject GameOver = (GameObject)Resources.Load("GameOverCanvas");
                GameObject.Instantiate(GameOver);
            }
            else
            {
                GetComponent<Enemy>().Die();
                Debug.Log("Enemy Dead!");
            }
            
        }
    }
}
