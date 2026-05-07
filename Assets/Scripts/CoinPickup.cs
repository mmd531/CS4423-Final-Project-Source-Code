using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int moneyAmount = 1;
    public AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (MoneyManager.Instance != null)
            {
                MoneyManager.Instance.AddMoney(moneyAmount);
            }

            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            Destroy(gameObject);
        }
    }
}