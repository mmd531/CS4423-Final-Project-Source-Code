using UnityEngine;
using TMPro;

public class SignPost : MonoBehaviour
{
    public GameObject signTextBox;
    public TextMeshProUGUI signText;
    [TextArea(2, 5)]
    public string message;

    void Start()
    {
        if (signTextBox != null)
        {
            signTextBox.SetActive(false);
        }

        if (signText != null)
        {
            signText.text = message;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (signText != null)
            {
                signText.text = message;
            }

            if (signTextBox != null)
            {
                signTextBox.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (signTextBox != null)
            {
                signTextBox.SetActive(false);
            }
        }
    }
}