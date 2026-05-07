using UnityEngine;

public class RoomCameraController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private Camera cam;
    private float targetSize;

    void Start()
    {
        cam = GetComponent<Camera>();
        targetPosition = transform.position;
        targetSize = cam.orthographicSize;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, moveSpeed * Time.deltaTime);
    }

    public void MoveToRoom(CameraRoom room)
    {
        targetPosition = new Vector3(room.cameraPosition.x, room.cameraPosition.y, transform.position.z);
        targetSize = room.cameraSize;
    }

    public void MoveToRoomInstant(CameraRoom room)
    {
        targetPosition = new Vector3(room.cameraPosition.x, room.cameraPosition.y, transform.position.z);
        targetSize = room.cameraSize;

        transform.position = targetPosition;
        cam.orthographicSize = targetSize;
    }
}
