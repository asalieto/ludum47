using UnityEngine;

public class Bullet : MonoBehaviour
{
    

    public void Init(Vector3 direction)
    {
        m_currentLifeTime = 0f;
        m_rb.AddForce(direction * m_bulletSpeed);

        m_isAlive = true;
    }

    void Update()
    {

        if(m_currentLifeTime >= m_lifeTime)
        {
            m_currentLifeTime = 0f;
            DestroyBullet();
        }
        else
        {
            m_currentLifeTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!m_isAlive)
        {
            return;
        }

        if (collision.CompareTag("Obstacle"))
        {
            m_isAlive = false;
            DestroyBullet();
        }

        if (m_isEnemyBullet)
        {
            if (collision.CompareTag("CatHeart"))
            {
                m_isAlive = false;
                AudioManager.Instance.PlaySFX(AudioManager.SFXType.Hit);
                collision.transform.parent.gameObject.GetComponent<HealthManager>().receiveDamage(1);
                DestroyBullet();
            }
        }
        else
        { 
            if (collision.CompareTag("Enemy"))
            {
                m_isAlive = false;
                AudioManager.Instance.PlaySFX(AudioManager.SFXType.Hit);
                collision.gameObject.GetComponent<HealthManager>().receiveDamage(1);
                DestroyBullet();
            }
        }
    }

    private void DestroyBullet()
    {
        GameObject.Destroy(this.gameObject);
    }

    private float m_currentLifeTime = 0f;

    [SerializeField]
    private float m_bulletSpeed = 10f;
    [SerializeField]
    private float m_lifeTime = 1f;
    [SerializeField]
    private Rigidbody2D m_rb = null;
    [SerializeField]
    private bool m_isEnemyBullet;

    private bool m_isAlive = true;
}
