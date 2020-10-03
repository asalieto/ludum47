using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    [SerializeField]
    private GameObject heart = null;

    private GameObject[] heartArray;

    private HealthManager playerHM;

    private int lastHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHM = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
        heartArray = new GameObject[playerHM.maxHealth];

        for (int i = 0; i < playerHM.maxHealth; i++)
        {
            heartArray[i] = Instantiate(heart);
            heartArray[i].transform.SetParent(transform);
            heartArray[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(i * heartArray[i].GetComponent<RectTransform>().rect.width, 0);
        }

        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                                                                heartArray[0].GetComponent<RectTransform>().rect.width* playerHM.maxHealth);

        lastHealth = playerHM.maxHealth;
    }

    // Update is called once per frame
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
