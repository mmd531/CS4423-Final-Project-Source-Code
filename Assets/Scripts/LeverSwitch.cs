using UnityEngine;
using UnityEngine.InputSystem;

public class LeverSwitch : MonoBehaviour
{
    public GameObject interactText;
    public DoorGate[] targetDoors;

    private bool playerInRange;
    private bool activated;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (interactText != null)
        {
            interactText.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame && !activated)
        {
            activated = true;

            if (interactText != null)
            {
                interactText.SetActive(false);
            }

            if (anim != null)
            {
                anim.SetBool("IsActivated", true);
            }

            for (int i = 0; i < targetDoors.Length; i++)
            {
                if (targetDoors[i] != null)
                {
                    targetDoors[i].OpenDoor();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !activated)
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
        }
    }
}