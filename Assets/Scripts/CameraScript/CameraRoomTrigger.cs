using UnityEngine;

public class CameraRoomTrigger : MonoBehaviour
{
    public CameraRoom targetRoom;

    private RoomCameraController roomCamera;

    void Start()
    {
        roomCamera = Camera.main.GetComponent<RoomCameraController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            roomCamera.MoveToRoom(targetRoom);
        }
    }
}
