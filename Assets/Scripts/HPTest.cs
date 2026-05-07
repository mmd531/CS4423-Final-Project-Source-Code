using UnityEngine;
using UnityEngine.InputSystem;

public class HPTest : MonoBehaviour
{
    private PlayerHP playerHP;

    void Start()
    {
        playerHP = GetComponent<PlayerHP>();
    }

    void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            playerHP.TakeDamage(10);
        }

        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            playerHP.Heal(10);
        }
    }
}
