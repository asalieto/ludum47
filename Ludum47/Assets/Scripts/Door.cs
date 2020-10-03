using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Enemy[] Enemies;
    public OverlapDetector[] Switches;
    public GameObject[] RoomSpecificRoot; // Default could be option 0
    public DoorCombination[] Combinations;
    public Transform StartPositionTransform;

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
        GameObject[] bullets =  GameObject.FindGameObjectsWithTag("Bullet");

        for(int i = 0; i < bullets.Length; ++i)
        {
            Destroy(bullets[i]);
        }

        RoomSpecificRoot[m_currentRoom].SetActive(false);
        m_currentRoom = CheckNextRoom();

        Debug.Log("TELEPORT TO ROOM: " + m_currentRoom);
        if(m_currentRoom == -1)
        {
            //Level win
            GameManager.Instance.LoadNextLevel();
        }
        else
        {
            RoomSpecificRoot[m_currentRoom].SetActive(true);

            for (int i = 0; i < Enemies.Length; ++i)
            {
                Enemies[i].ResetEnemy();
            }

            m_enemiesDieOrdered.Clear();
        }
    }
    
    private int CheckNextRoom()
    {
        for(int i = 0; i < Combinations.Length; ++i)
        {
            //FOR CHECKING ENEMIES
            if (Combinations[i].CodeOpen.Count > 0)
            {
                for (int combiIter = 0; combiIter < Combinations[i].CodeOpen.Count; ++combiIter)
                {
                    if (Combinations[i].CodeOpen.Count > m_enemiesDieOrdered.Count)
                    {
                        break;
                    }

                    if (Combinations[i].NeedOrder &&
                        Combinations[i].CodeOpen[combiIter] != m_enemiesDieOrdered[combiIter])
                    {
                        break;
                    }
                    else if (!m_enemiesDieOrdered.Contains(Combinations[i].CodeOpen[combiIter]))
                    {
                        break;
                    }

                    if (combiIter == Combinations[i].CodeOpen.Count - 1)
                    {
                        if (Combinations[i].NeedOtherAlive)
                        {
                            bool combiValid = true;
                            for (int enemyIter = 0; enemyIter < Enemies.Length; ++enemyIter)
                            {
                                if (!Combinations[i].CodeOpen.Contains(Enemies[enemyIter].EnemyID) &&
                                    !Enemies[enemyIter].IsAlive())
                                {
                                    combiValid = false;
                                    break;
                                }
                            }

                            if (!combiValid)
                            {
                                break;
                            }
                        }

                        if (Combinations[i].FinalComb)
                        {
                            return -1;
                        }
                        else
                        {
                            return Combinations[i].DoorOpen;
                        }
                    }
                }
            }

            //FOR CHECKING SWITCHES
            if (Combinations[i].CodeSwitchOpen.Count > 0)
            {
                for (int combiIter = 0; combiIter < Combinations[i].CodeSwitchOpen.Count; ++combiIter)
                {
                    bool combiValid = true;
                    for (int j = 0; j < Switches.Length; j++)
                    {
                        if (Switches[j].IsActive() && Switches[j].id == Combinations[i].CodeSwitchOpen[combiIter])
                        {
                            combiValid = true;
                        }
                        else
                        {
                            combiValid = false;
                            break;
                        }
                    }

                    if (combiValid)
                    {
                        if (Combinations[i].FinalComb)
                        {
                            return -1;
                        }
                        else
                        {
                            return Combinations[i].DoorOpen;
                        }
                    }
                }
            }

            if(Combinations[i].CodeOpen.Count == 0 && Combinations[i].CodeSwitchOpen.Count == 0)
            {
                return -1;
            }
        }

        return 0;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            TransportToNextRoom();

            //This should be done on the player
            col.gameObject.transform.position = StartPositionTransform.position;
        }
    }

}


