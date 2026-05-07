using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    void Start()
    {
        HideGameOver();
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void HideGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void RetryAtCheckpoint()
    {
        Time.timeScale = 1f;

        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.RetryAtCheckpoint();
        }
        else
        {
            Debug.LogError("CheckpointManager is missing.");
        }
    }

    public void ReturnToHub()
    {
        Time.timeScale = 1f;

        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.ReturnToHub();
        }
        else
        {
            Debug.LogError("CheckpointManager is missing.");
        }
    }
}
