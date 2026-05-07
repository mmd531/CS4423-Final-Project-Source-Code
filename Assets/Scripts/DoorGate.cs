using UnityEngine;

public class DoorGate : MonoBehaviour
{
    public bool destroyOnOpen = true;

    public void OpenDoor()
    {
        if (destroyOnOpen)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}