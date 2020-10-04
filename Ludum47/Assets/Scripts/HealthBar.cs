using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject heart = null;

    private GameObject[] heartArray;
    private HealthManager playerHM;
    private int lastHealth;

    void Start()
    {
        playerHM = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
        heartArray = new GameObject[playerHM.maxHealth];

        for (int i = 0; i < playerHM.maxHealth; i++)
        {
            heartArray[i] = Instantiate(heart, this.transform);
        }

        lastHealth = playerHM.maxHealth;
    }

    void Update()
    {
        if(lastHealth != playerHM.currentHealth)
        {
            lastHealth = playerHM.maxHealth;
            for (int i = 0; i < playerHM.maxHealth; i++)
            {
                if(i > playerHM.currentHealth - 1)
                {
                    heartArray[i].SetActive(false);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) playerHM.receiveDamage(1);
    }
}
