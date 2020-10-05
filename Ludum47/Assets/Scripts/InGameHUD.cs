using UnityEngine;

public class InGameHUD : MonoBehaviour
{
    private GameObject m_player;

    [SerializeField]
    private GameObject m_mobileUI = null;
    [SerializeField]
    private Joystick m_joystick = null;
    [SerializeField]
    private bool m_useJoystick = false;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        if (m_useJoystick && m_joystick != null)
        {
            m_mobileUI.SetActive(true);
            GameManager.Instance.Joystick = m_joystick;
        }
        else
        {
            m_mobileUI.SetActive(false);
        }
    }

    public void TryShoot()
    {
        m_player.GetComponent<PlayerMovement>().TryShoot();
    }

    public void TryUse()
    {
        m_player.GetComponent<GrabItems>().TryGrabOrUse();
    }
}
