using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 10;
    public float damageCooldown = 1f;

    private bool canDamage = true;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canDamage)
        {
            PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>();

            if (playerHP != null)
            {
                playerHP.TakeDamage(damageAmount);
                StartCoroutine(DamageCooldown());
            }
        }
    }

    IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}