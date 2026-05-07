using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 50;
    public float knockbackForce = 6f;
    public float knockbackTime = 0.2f;

    private int currentHealth;
    private Rigidbody2D rb;
    private EnemyPatrol enemyPatrol;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    public void TakeDamage(int damage, Vector2 hitDirection)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(Knockback(hitDirection));
    }

    IEnumerator Knockback(Vector2 hitDirection)
    {
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(hitDirection.normalized * knockbackForce, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(knockbackTime);

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = true;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
