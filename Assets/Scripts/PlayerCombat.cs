using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public Transform shootPoint;

    public float pointDistance = 0.7f;

    public float meleeRange = 0.5f;
    public int meleeDamage = 20;
    public float meleeDelay = 0.15f;
    public LayerMask enemyLayer;

    public GameObject projectilePrefab;
    public int projectileDamage = 10;
    public float projectileSpeed = 12f;
    public float shootDelay = 0.25f;

    private SpriteRenderer sr;
    private Animator anim;
    private bool isShooting;
    private bool isMeleeAttacking;
    private float attackMultiplier = 1f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdatePointPositions();

        if (Mouse.current.leftButton.wasPressedThisFrame && !isMeleeAttacking)
        {
            StartCoroutine(MeleeWithDelay());
        }

        if (Mouse.current.rightButton.wasPressedThisFrame && !isShooting)
        {
            StartCoroutine(ShootWithDelay());
        }
    }

    void UpdatePointPositions()
    {
        float direction = sr.flipX ? -1f : 1f;

        attackPoint.localPosition = new Vector3(direction * pointDistance, attackPoint.localPosition.y, attackPoint.localPosition.z);
        shootPoint.localPosition = new Vector3(direction * pointDistance, shootPoint.localPosition.y, shootPoint.localPosition.z);
    }

    IEnumerator MeleeWithDelay()
    {
        isMeleeAttacking = true;

        if (anim != null)
        {
            anim.SetTrigger("IsMelee");
        }

        yield return new WaitForSeconds(meleeDelay);

        MeleeAttack();

        yield return new WaitForSeconds(0.2f);

        isMeleeAttacking = false;
    }

    void MeleeAttack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, meleeRange, enemyLayer);

        for (int i = 0; i < enemiesHit.Length; i++)
        {
            EnemyHP enemyHP = enemiesHit[i].GetComponent<EnemyHP>();

            if (enemyHP != null)
            {
                int finalDamage = Mathf.RoundToInt(meleeDamage * attackMultiplier);
                Vector2 hitDirection = enemiesHit[i].transform.position - transform.position;
                enemyHP.TakeDamage(finalDamage, hitDirection);
            }
        }
    }

    IEnumerator ShootWithDelay()
    {
        isShooting = true;

        if (anim != null)
        {
            anim.SetTrigger("IsShooting");
        }

        yield return new WaitForSeconds(shootDelay);

        ShootProjectile();

        isShooting = false;
    }

    void ShootProjectile()
    {
        float direction = sr.flipX ? -1f : 1f;

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.damage = Mathf.RoundToInt(projectileDamage * attackMultiplier);
        }

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(direction * projectileSpeed, 0f);
        }

        Vector3 scale = projectile.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        projectile.transform.localScale = scale;
    }

    public void SetAttackMultiplier(float newMultiplier)
    {
        attackMultiplier = newMultiplier;
        Debug.Log("Attack Multiplier: " + attackMultiplier);
    }

    public float GetAttackMultiplier()
    {
        return attackMultiplier;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, meleeRange);
        }
    }
}