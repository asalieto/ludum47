using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Door CurrentPortal;
    public int m_currentLevel = 0;

    public void LoadNextLevel()
    {
        m_currentLevel++;
        SceneManager.LoadScene("Level" + m_currentLevel.ToString(), LoadSceneMode.Single);
        //Application.LoadLevel("Level" + m_currentLevel.ToString());
    }
}