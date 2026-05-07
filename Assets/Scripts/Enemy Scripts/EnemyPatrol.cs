using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public float moveSpeed = 2f;
    public float chaseSpeed = 3f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Transform player;
    private bool movingRight = true;
    private bool playerInZone;
    private float lastDirection = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void FixedUpdate()
    {
        if (leftPoint == null || rightPoint == null)
        {
            return;
        }

        CheckPlayerZone();

        if (playerInZone)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void CheckPlayerZone()
    {
        if (player == null)
        {
            playerInZone = false;
            return;
        }

        float leftX = Mathf.Min(leftPoint.position.x, rightPoint.position.x);
        float rightX = Mathf.Max(leftPoint.position.x, rightPoint.position.x);

        playerInZone = player.position.x >= leftX && player.position.x <= rightX;
    }

    void ChasePlayer()
    {
        float distanceToPlayer = player.position.x - transform.position.x;
        float direction;

        if (distanceToPlayer > 0f)
        {
            direction = 1f;
        }
        else if (distanceToPlayer < 0f)
        {
            direction = -1f;
        }
        else
        {
            direction = lastDirection;
        }

        lastDirection = direction;
        movingRight = direction > 0f;

        rb.linearVelocity = new Vector2(direction * chaseSpeed, rb.linearVelocity.y);

        if (sr != null)
        {
            sr.flipX = direction < 0f;
        }
    }

    void Patrol()
    {
        if (movingRight)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
            lastDirection = 1f;

            if (sr != null)
            {
                sr.flipX = false;
            }

            if (transform.position.x >= rightPoint.position.x)
            {
                movingRight = false;
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
            lastDirection = -1f;

            if (sr != null)
            {
                sr.flipX = true;
            }

            if (transform.position.x <= leftPoint.position.x)
            {
                movingRight = true;
            }
        }
    }
}