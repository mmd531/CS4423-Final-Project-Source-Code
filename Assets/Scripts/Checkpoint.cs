using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform respawnPoint;
    public CameraRoom checkpointRoom;

    private bool activated;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;

            if (CheckpointManager.Instance != null)
            {
                Vector3 positionToSave = transform.position;

                if (respawnPoint != null)
                {
                    positionToSave = respawnPoint.position;
                }

                CheckpointManager.Instance.SetCheckpoint(positionToSave, checkpointRoom);
            }
        }
    }
}