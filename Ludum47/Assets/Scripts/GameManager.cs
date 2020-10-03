using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int m_currentLevel = 0;
    /*
    private void Start()
    {
        //lol this is terrible, TODO use a loader instead
        var a = Instance;
    }
    */
    public void LoadNextLevel()
    {
        m_currentLevel++;
        SceneManager.LoadScene("Level" + m_currentLevel.ToString(), LoadSceneMode.Single);
        //Application.LoadLevel("Level" + m_currentLevel.ToString());
    }
}