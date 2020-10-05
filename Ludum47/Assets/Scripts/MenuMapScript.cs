using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMapScript : MonoBehaviour
{
    private void Start()
    {
        if(Application.platform != RuntimePlatform.WindowsPlayer)
        {
            m_exitGO.SetActive(false);
        }

        OpenMainMenu();
    }

    public void PlayLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void OpenMainMenu()
    {
        m_mainMenuGO.SetActive(true);
        m_levelSelectGO.SetActive(false);
        m_creditsGO.SetActive(false);
    }
    public void OpenLevelSelection()
    {
        m_mainMenuGO.SetActive(false);
        m_levelSelectGO.SetActive(true);
        m_creditsGO.SetActive(false);
    }

    public void OpenCredits()
    {
        m_mainMenuGO.SetActive(false);
        m_levelSelectGO.SetActive(false);
        m_creditsGO.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    [SerializeField]
    private string m_initialLevel;

    [SerializeField]
    private GameObject m_mainMenuGO;
    [SerializeField]
    private GameObject m_levelSelectGO;
    [SerializeField]
    private GameObject m_creditsGO;
    [SerializeField]
    private GameObject m_exitGO;
}
