using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Enemy[] Enemies;
    public GameObject[] RoomSpecificRoot; // Default could be option 0
    public DoorCombination[] Combinations;

    private int m_currentRoom = 0;
    private List<int> m_enemiesDieOrdered = new List<int>();


    private void Start()
    {
        for(int i = 0; i < Enemies.Length; ++i)
        {
            Enemies[i].OnDie = OnDieEnemy;
        }
    }


    private void OnDieEnemy(int id)
    {
        m_enemiesDieOrdered.Add(id);
    }

    private void TransportToNextRoom()
    {
        RoomSpecificRoot[m_currentRoom].SetActive(false);
        m_currentRoom = CheckNextRoom();
        RoomSpecificRoot[m_currentRoom].SetActive(true);

        for(int i = 0;  i < Enemies.Length; ++i)
        {
            Enemies[i].ResetEnemy();
        }

        m_enemiesDieOrdered.Clear();
    }
    
    private int CheckNextRoom()
    {
        for(int i = 0; i < Combinations.Length; ++i)
        {
            for(int combiIter = 0; combiIter < Combinations[i].CodeOpen.Count; ++combiIter)
            {
                if(Combinations[i].CodeOpen.Count > m_enemiesDieOrdered.Count)
                {
                    break;
                }

                if(Combinations[i].NeedOrder && 
                    Combinations[i].CodeOpen[combiIter] != m_enemiesDieOrdered[combiIter])
                {
                    break;
                }
                else if (!m_enemiesDieOrdered.Contains(Combinations[i].CodeOpen[combiIter]))
                {
                    break;
                }

                if(combiIter == Combinations[i].CodeOpen.Count - 1)
                {
                    if(Combinations[i].NeedOtherAlive)
                    {
                        bool combiValid = true;
                        for (int enemyIter = 0; enemyIter < Enemies.Length; ++enemyIter)
                        {
                            if(!Combinations[i].CodeOpen.Contains(Enemies[enemyIter].EnemyID) &&
                                !Enemies[enemyIter].IsAlive())
                            {
                                combiValid = false;
                                break;
                            }
                        }

                        if(!combiValid)
                        {
                            break;
                        }
                    }

                    return Combinations[i].DoorOpen;
                }
            }
        }

        return 0;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            TransportToNextRoom();

            //This should be donde on the player
            col.gameObject.transform.position = new Vector3(0, 5, 5);
        }
    }

}


