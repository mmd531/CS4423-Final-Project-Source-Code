using UnityEngine;
using UnityEngine.InputSystem;

public class WorldItem : MonoBehaviour
{
    public ItemData itemData;
    public int amount = 1;
    public GameObject pickupText;

    private bool playerInRange;

    void Start()
    {
        if (pickupText != null)
        {
            pickupText.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            PickupItem();
        }
    }

    void PickupItem()
    {
        if (HotbarManager.Instance == null)
        {
            return;
        }

        bool pickedUp = HotbarManager.Instance.AddItem(itemData, amount);

        if (pickedUp)
        {
            if (pickupText != null)
            {
                pickupText.SetActive(false);
            }

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (pickupText != null)
            {
                pickupText.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (pickupText != null)
            {
                pickupText.SetActive(false);
            }
        }
    }
}
