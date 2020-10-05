using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public bool IsAlive => alive;

    public int maxHealth = 3;
    public int currentHealth;

    private bool alive = true;
    private Vector3 initialPosition;
    private SpriteRenderer m_spriteRenderer;
    private Coroutine m_damageRoutine = null;
    private float m_currentColorDamageDuration = 0f;

    [SerializeField] 
    private GameObject m_hitParticlesPrefab = null;
    [SerializeField]
    private Color m_damagedColor = Color.white;
    [SerializeField]
    private float m_colorDamageDuration = 0.5f;

    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
        initialPosition = transform.position;
    }

    public void Respawn()
    {
        transform.position = initialPosition;
        alive = true;
        currentHealth = maxHealth;
    }

    public void receiveDamage(int damage)
    {
        currentHealth -= damage;

        if (m_hitParticlesPrefab != null)
        {
            GameObject.Instantiate(m_hitParticlesPrefab, this.transform);
        }

        VisualDamage();

        if (currentHealth <= 0 && alive)
        {
            if(tag == "Player")
            {
                GameObject GameOver = (GameObject)Resources.Load("GameOverCanvas");
                GameObject.Instantiate(GameOver);
            }
            else
            {
                GetComponent<Enemy>().Die();
            }

            alive = false;
        }
    }

    private void VisualDamage()
    {
        if (m_damageRoutine != null)
        {
            StopCoroutine(m_damageRoutine);
            m_damageRoutine = null;
            m_spriteRenderer.color = Color.white;
        }

        m_damageRoutine = StartCoroutine(VisualDamageRoutine());
    }

    private IEnumerator VisualDamageRoutine()
    {
        m_currentColorDamageDuration = 0f;

        m_spriteRenderer.color = m_damagedColor;

        while (m_currentColorDamageDuration < m_colorDamageDuration)
        {
            m_spriteRenderer.color = Color.Lerp(m_damagedColor ,Color.white, m_currentColorDamageDuration / m_colorDamageDuration);

            yield return null;

            m_currentColorDamageDuration += Time.deltaTime;
        }
    }
}
