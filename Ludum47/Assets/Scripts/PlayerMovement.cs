using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerHM = GetComponent<HealthManager>();
        animator = GetComponent<Animator>();
        grabItems = GetComponent<GrabItems>();
    }

    void Update()
    {
        Vector2 auxVel = new Vector2(0.0f, 0.0f);

        if (playerHM.IsAlive)
        {
            if (!m_useJoystick)
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    auxVel.y += speed;
                }
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    auxVel.y -= speed;
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    auxVel.x += speed;
                }
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    auxVel.x -= speed;
                }
            }
            else
            {
                auxVel = GameManager.Instance.Joystick.Direction * speed;
            }
        }

        playerRB.velocity = auxVel;
        animator.SetFloat("SpeedX", auxVel.x);
        animator.SetFloat("SpeedY", auxVel.y);

        if(playerRB.velocity.magnitude != 0f)
        {
            m_lastVelocityDirection = playerRB.velocity.normalized;
            m_lastVelocityDirection.Normalize();

            angle = Mathf.Atan2(playerRB.velocity.y, playerRB.velocity.x) * Mathf.Rad2Deg;
        }

        m_currentBulletInterval += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            TryShoot();
        }
    }

    public void TryShoot()
    {
        if (m_currentBulletInterval > m_bulletInterval && !grabItems.HoldingItem)
        {
            Shoot();
            m_currentBulletInterval = 0f;
        }
    }

    private void Shoot()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Projectile, true);

        var bullet = Instantiate(m_bulletPrefab, transform.position + new Vector3(m_lastVelocityDirection.x, m_lastVelocityDirection.y, 0) * m_bulletSeparationMultiplier, Quaternion.AngleAxis(angle, Vector3.forward));
        bullet.GetComponent<Bullet>().Init(m_lastVelocityDirection.normalized);
        animator.SetTrigger("Shoot");
    }

    public float speed = 5f;

    private Rigidbody2D playerRB;
    private HealthManager playerHM;
    private Animator animator;
    private GrabItems grabItems;
    private Vector2 m_lastVelocityDirection = Vector2.down;
    private float angle = 0;
    private float m_currentBulletInterval = 0f;

    [SerializeField]
    private bool m_useJoystick = false;
    [SerializeField]
    private GameObject m_bulletPrefab = null;
    [SerializeField]
    private float m_bulletInterval = 0.2f;
    [SerializeField]
    private float m_bulletSeparationMultiplier = 0.02f;
}
