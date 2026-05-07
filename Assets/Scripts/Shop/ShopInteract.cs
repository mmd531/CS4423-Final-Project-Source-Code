using UnityEngine;
using UnityEngine.InputSystem;

public class ShopInteract : MonoBehaviour
{
    public GameObject interactText;
    public ShopManager shopManager;

    private bool playerInRange;

    void Start()
    {
        if (interactText != null)
        {
            interactText.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (shopManager != null)
            {
                shopManager.ToggleShop();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (interactText != null)
            {
                interactText.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactText != null)
            {
                interactText.SetActive(false);
            }

            if (shopManager != null)
            {
                shopManager.CloseShop();
            }
        }
    }
}