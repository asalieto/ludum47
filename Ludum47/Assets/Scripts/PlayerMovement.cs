using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    public float speed = 5f;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        Vector2 auxVel = new Vector2(0.0f, 0.0f);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            auxVel.y += speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            auxVel.y -= speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            auxVel.x += speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            auxVel.x -= speed;
        }

        playerRB.velocity = auxVel;
        /*transform.position.Set(transform.position.x + auxVel.x * Time.deltaTime,
                               transform.position.y + auxVel.y * Time.deltaTime,
                               transform.position.z);*/

        if(playerRB.velocity.magnitude != 0f)
        {
            _lastVelocityDirection = playerRB.velocity;

            float angle = Mathf.Atan2(playerRB.velocity.y, playerRB.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        _currentBulletInterval += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            if(_currentBulletInterval > _bulletInterval)
            {
                Shoot();
                _currentBulletInterval = 0f;
            }
        }
    }

    private void Shoot()
    {
        var bullet = GameObject.Instantiate(_bulletPrefab, this.transform.position + (transform.up * _bulletSeparationMultiplier), Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(_lastVelocityDirection.normalized);
    }

    private Vector2 _lastVelocityDirection = Vector2.up;
    private float _currentBulletInterval = 0f;

    [SerializeField]
    private GameObject _bulletPrefab = null;
    [SerializeField]
    private float _bulletInterval = 0.2f;
    [SerializeField]
    private float _bulletSeparationMultiplier = 0.02f;
}
