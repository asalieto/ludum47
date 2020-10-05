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
    public SpriteRenderer[] m_portalBackgroud;
    public ParticleSystem m_particles;

    private int m_currentRoom = 0; 
    private int m_previousRoom = 0;
    private List<int> m_enemiesDieOrdered = new List<int>();

    private Color m_defaultColor;

    private void Start()
    {
        for(int i = 0; i < Enemies.Length; ++i)
        {
            Enemies[i].OnDie = OnDieEnemy;
        }

        for (int i = 0; i < Switches.Length; ++i)
        {
            Switches[i].OnChangeState = TryChangeColor;
        }

        m_defaultColor = m_portalBackgroud[0].color;
        GameManager.Instance.CurrentPortal = this;

        TryChangeColor();
    }

    public GameObject GetCurrentRoomGO()
    {
        return RoomSpecificRoot[m_currentRoom];
    }

    private void OnDieEnemy(int id)
    {
        m_enemiesDieOrdered.Add(id);
        TryChangeColor();
    }

    private void TransportToNextRoom()
    {
        RoomSpecificRoot[m_previousRoom].SetActive(false);
        RoomSpecificRoot[m_currentRoom].SetActive(true);

        for (int i = 0; i < Enemies.Length; ++i)
        {
            Enemies[i].ResetEnemy();
        }

        m_enemiesDieOrdered.Clear();

        TryChangeColor();
    }

    private void TryChangeColor()
    {
        int nextRoom = CheckNextRoom();

        Color doorColor = GetRoomColor(nextRoom);
            
        for (int i = 0; i < m_portalBackgroud.Length; ++i)
        {
            doorColor.a = m_defaultColor.a;
            m_portalBackgroud[i].color = doorColor;
            m_particles.startColor = doorColor;
        }
    }

    private Color GetRoomColor(int room)
    {
        switch (room)
        {
            case -1:
                return Color.yellow;
            case 1:
                return Color.green;
            case 2:
                return Color.cyan;
            case 3:
                return Color.magenta;
            case 4:   // I think don't exist yet
                return Color.red;
            default:
                return m_defaultColor;

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
            else if (Combinations[i].NeedOtherAlive)
            {
                bool combiValid = true;
                for (int enemyIter = 0; enemyIter < Enemies.Length; ++enemyIter)
                {
                    if (!Enemies[enemyIter].IsAlive())
                    {
                        combiValid = false;
                        break;
                    }
                }

                if (!combiValid)
                {
                    break;
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


            //FOR CHECKING SWITCHES
            if (Combinations[i].CodeSwitchOpen.Count > 0)
            {
                bool combiValid = true;
                for (int j = 0; j < Switches.Length; j++)
                {
                    if(Combinations[i].CodeSwitchOpen.Contains(Switches[j].id))
                    {
                        if (Switches[j].IsActive())
                        {
                            combiValid = true;
                        }
                        else
                        {
                            combiValid = false;
                            break;
                        }
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

            if(Combinations[i].CodeOpen.Count == 0 && Combinations[i].CodeSwitchOpen.Count == 0 && Combinations[i].FinalComb)
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
            UsePortal(col.gameObject);
        }
    }

    private void UsePortal(GameObject playerGO)
    {
        m_previousRoom = m_currentRoom;
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        for (int i = 0; i < bullets.Length; ++i)
        {
            Destroy(bullets[i]);
        }

        m_currentRoom = CheckNextRoom();

        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Portal);

        if (m_currentRoom == -1)
        {
            //Level win
            GameManager.Instance.LoadNextLevel();
        }
        else
        {
            //This should be done on the player
            playerGO.gameObject.transform.position = StartPositionTransform.position;
            TransportToNextRoom();
        }
    }
}


