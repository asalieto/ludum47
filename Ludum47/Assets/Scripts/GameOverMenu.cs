using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void Retry()
    {
        GameManager.Instance.Retry();
        Destroy(gameObject);
    }
    public void Exit()
    {
        SceneManager.LoadScene("MenuMap", LoadSceneMode.Single);
    }
}
