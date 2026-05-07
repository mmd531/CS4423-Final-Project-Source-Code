using System.Collections;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int maxHealth = 100;
    public float gameOverDelay = 1.2f;

    public AudioClip deathSound;
    public float deathSoundVolume = 1f;

    public AudioClip hurtSound;
    public float hurtSoundVolume = 1f;

    private int currentHealth;
    private HealthBarUI healthBarUI;
    private Animator anim;
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private Rigidbody2D rb;
    private bool isDead;

    void Start()
    {
        currentHealth = maxHealth;

        healthBarUI = FindFirstObjectByType<HealthBarUI>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        rb = GetComponent<Rigidbody2D>();

        if (healthBarUI != null)
        {
            healthBarUI.SetMaxHealth(maxHealth);
            healthBarUI.SetHealth(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (healthBarUI != null)
        {
            healthBarUI.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            if (hurtSound != null)
            {
                AudioSource.PlayClipAtPoint(hurtSound, transform.position, hurtSoundVolume);
            }

            if (anim != null)
            {
                anim.SetTrigger("IsHurt");
            }
        }
    }

    public void Heal(int amount)
    {
        if (isDead)
        {
            return;
        }

        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (healthBarUI != null)
        {
            healthBarUI.SetHealth(currentHealth);
        }
    }

    void Die()
    {
        isDead = true;

        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.LoseHalfMoney();
        }

        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
        }

        if (anim != null)
        {
            anim.SetBool("IsDead", true);
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (playerCombat != null)
        {
            playerCombat.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        StartCoroutine(ShowGameOverAfterDelay());
    }

    IEnumerator ShowGameOverAfterDelay()
    {
        yield return new WaitForSeconds(gameOverDelay);

        GameOverManager gameOverManager = FindFirstObjectByType<GameOverManager>();

        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
    }

    public void ResetHP()
    {
        isDead = false;
        currentHealth = maxHealth;

        if (healthBarUI != null)
        {
            healthBarUI.SetHealth(currentHealth);
        }

        if (anim != null)
        {
            anim.SetBool("IsDead", false);
            anim.ResetTrigger("IsHurt");
            anim.Play("Player_Idle");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}