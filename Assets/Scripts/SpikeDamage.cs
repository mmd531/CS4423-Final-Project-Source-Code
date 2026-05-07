using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damageAmount = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHP playerHP = other.GetComponent<PlayerHP>();

            if (playerHP != null)
            {
                playerHP.TakeDamage(damageAmount);
            }
        }
    }
}
