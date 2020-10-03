using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMapScript : MonoBehaviour
{
    public void PlayTheGame()
    {
        SceneManager.LoadScene(m_initialLevel);
    }

    [SerializeField]
    private string m_initialLevel;
}
