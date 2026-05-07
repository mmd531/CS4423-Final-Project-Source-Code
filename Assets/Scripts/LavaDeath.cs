using UnityEngine;

public class LavaDeath : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHP playerHP = other.GetComponent<PlayerHP>();

            if (playerHP != null)
            {
                playerHP.TakeDamage(playerHP.GetCurrentHealth());
            }
        }
    }
}
