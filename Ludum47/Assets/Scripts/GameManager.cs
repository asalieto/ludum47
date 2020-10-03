using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Door CurrentPortal;
    public int m_currentLevel = 0;

    private List<int> m_checkPoints;

    
    private void Start()
    {
        // Create check point list
        m_checkPoints = new List<int>();

        m_checkPoints.Add(0);
        m_checkPoints.Add(2);
        m_checkPoints.Add(5);
        // ........
    }
    
    public void LoadNextLevel()
    {
        m_currentLevel++;
        SceneManager.LoadScene("Level" + m_currentLevel.ToString(), LoadSceneMode.Single);
    }

    public void Retry()
    {

        if (m_currentLevel > m_checkPoints[m_checkPoints.Count-1])
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

            m_currentLevel = m_checkPoints[iter] - 1;
        }

        LoadNextLevel();
    }
}