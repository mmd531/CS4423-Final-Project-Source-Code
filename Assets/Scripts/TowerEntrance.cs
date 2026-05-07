using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TowerEntrance : MonoBehaviour
{
    public string nextSceneName;
    public GameObject enterText;

    private bool playerInRange;

    void Start()
    {
        if (enterText != null)
        {
            enterText.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (enterText != null)
            {
                enterText.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (enterText != null)
            {
                enterText.SetActive(false);
            }
        }
    }
}
