using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    public RectTransform hpBarMask;
    public float maxWidth = 200f;

    private int maxHealth = 100;

    public void SetMaxHealth(int health)
    {
        maxHealth = health;
    }

    public void SetHealth(int currentHealth)
    {
        float healthPercent = (float)currentHealth / maxHealth;

        if (healthPercent < 0f)
        {
            healthPercent = 0f;
        }

        if (healthPercent > 1f)
        {
            healthPercent = 1f;
        }

        hpBarMask.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth * healthPercent);
    }
}
