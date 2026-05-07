using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHP enemyHP = other.GetComponent<EnemyHP>();

        if (enemyHP != null)
        {
            Vector2 hitDirection = other.transform.position - transform.position;
            enemyHP.TakeDamage(damage, hitDirection);
            Destroy(gameObject);
            return;
        }
    }
}
