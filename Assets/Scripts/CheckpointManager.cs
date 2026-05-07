using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    public Vector3 currentCheckpointPosition;
    public CameraRoom currentCheckpointRoom;
    public string hubSceneName = "Scene1";

    private GameObject player;
    private PlayerHP playerHP;
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private Rigidbody2D playerRb;
    private Animator playerAnim;
    private RoomCameraController roomCamera;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FindPlayer();
        roomCamera = FindFirstObjectByType<RoomCameraController>();

        if (player != null)
        {
            currentCheckpointPosition = player.transform.position;
        }
    }

    void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerHP = player.GetComponent<PlayerHP>();
            playerMovement = player.GetComponent<PlayerMovement>();
            playerCombat = player.GetComponent<PlayerCombat>();
            playerRb = player.GetComponent<Rigidbody2D>();
            playerAnim = player.GetComponent<Animator>();
        }
    }

    public void SetCheckpoint(Vector3 newCheckpointPosition, CameraRoom checkpointRoom)
    {
        currentCheckpointPosition = newCheckpointPosition;
        currentCheckpointRoom = checkpointRoom;

        Debug.Log("Checkpoint set to " + currentCheckpointPosition);
    }

    public void RetryAtCheckpoint()
    {
        if (player == null)
        {
            FindPlayer();
        }

        if (roomCamera == null)
        {
            roomCamera = FindFirstObjectByType<RoomCameraController>();
        }

        if (player == null)
        {
            Debug.LogError("No player found for respawn.");
            return;
        }

        player.transform.position = currentCheckpointPosition;

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
        }

        if (playerHP != null)
        {
            playerHP.ResetHP();
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        if (playerCombat != null)
        {
            playerCombat.enabled = true;
        }

        if (playerAnim != null)
        {
            playerAnim.SetBool("IsDead", false);
            playerAnim.Play("Player_Idle");
        }

        if (roomCamera != null && currentCheckpointRoom != null)
        {
            roomCamera.MoveToRoomInstant(currentCheckpointRoom);
        }

        GameOverManager gameOverManager = FindFirstObjectByType<GameOverManager>();

        if (gameOverManager != null)
        {
            gameOverManager.HideGameOver();
        }
    }

    public void ReturnToHub()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(hubSceneName);
    }
}
