using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Joystick Joystick { get; set; }
    public Door CurrentPortal { get; set; }

    private int m_currentLevel = 0;
    private List<int> m_checkPoints;

    private const int MAX_LEVEL = 10; 

    private void Start()
    {
        // Create check point list
        m_checkPoints = new List<int>();

        m_checkPoints.Add(0);
        m_checkPoints.Add(3);
        m_checkPoints.Add(6);
        m_checkPoints.Add(8);
        m_checkPoints.Add(9);
        // ........

        AudioManager.Instance.PlayAudio(0, true);
    }
    
    public void LoadNextLevel()
    {
        m_currentLevel++;

        if(m_currentLevel > MAX_LEVEL)
        {
            SceneManager.LoadScene("MenuMap", LoadSceneMode.Single);
            return;
        }

        SceneManager.LoadScene("Level" + m_currentLevel.ToString(), LoadSceneMode.Single);
    }

    public void Retry()
    {
        if (m_currentLevel >= m_checkPoints[m_checkPoints.Count-1])
        {
            m_currentLevel = m_checkPoints[m_checkPoints.Count - 1];
        }
        else
        {
            int iter = 0;

            while (m_currentLevel >= m_checkPoints[iter])
            {
                iter++;
            }

            iter--;

            m_currentLevel = m_checkPoints[iter];
        }

        m_currentLevel--;
        LoadNextLevel();
    }
}