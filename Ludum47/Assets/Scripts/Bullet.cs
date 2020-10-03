using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void Init(Vector3 direction)
    {
        m_rb.AddForce(direction * m_bulletSpeed);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            DestroyBullet();
        }

        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        GameObject.Destroy(this.gameObject);
    }

    [SerializeField]
    private float m_bulletSpeed = 10f;
    [SerializeField]
    private Rigidbody2D m_rb = null;
}
