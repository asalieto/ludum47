using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void Init(Vector3 direction)
    {
        _rb.AddForce(direction * _bulletSpeed);
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
    private float _bulletSpeed = 10f;
    [SerializeField]
    private Rigidbody2D _rb = null;
}
