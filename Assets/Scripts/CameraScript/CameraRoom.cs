using UnityEngine;

public class CameraRoom : MonoBehaviour
{
    public Vector3 cameraPosition;
    public float cameraSize = 5f;
    public float aspectRatio = 16f / 9f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        float height = cameraSize * 2f;
        float width = height * aspectRatio;

        Gizmos.DrawWireCube(cameraPosition, new Vector3(width, height, 0f));
    }
}
