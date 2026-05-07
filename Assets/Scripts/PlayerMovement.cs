using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpForce = 12f;
    public float stopSlideTime = 0.15f;
    public float slideSlowdown = 20f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private float moveInput;
    private float currentSpeed;
    private float stopSlideTimer;
    private bool isGrounded;
    private bool wasMoving;
    private bool wasSprinting;
    private bool keepSprintMomentum;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInput = -1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            moveInput = 1f;
        }
        else
        {
            moveInput = 0f;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        bool shiftHeld = Keyboard.current.leftShiftKey.isPressed || Keyboard.current.rightShiftKey.isPressed;
        bool isMoving = Mathf.Abs(moveInput) > 0.01f;
        bool isSprinting = shiftHeld && isMoving && isGrounded;

        if (!isGrounded && wasSprinting)
        {
            keepSprintMomentum = true;
        }

        if (isGrounded)
        {
            keepSprintMomentum = false;
        }

        if (wasSprinting && !isSprinting && !isMoving)
        {
            stopSlideTimer = stopSlideTime;
        }

        if (isSprinting || keepSprintMomentum)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (moveInput > 0)
        {
            sr.flipX = false;
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;
        }

        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("IsSprinting", isSprinting);

        if (wasMoving && !isMoving && isGrounded)
        {
            anim.SetBool("IsStopping", true);
        }
        else if (stopSlideTimer <= 0f || !isGrounded)
        {
            anim.SetBool("IsStopping", false);
        }

        wasMoving = isMoving;
        wasSprinting = isSprinting;
    }

    void FixedUpdate()
    {
        bool isTryingToMove = Mathf.Abs(moveInput) > 0.01f;

        if (isTryingToMove)
        {
            rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);
            stopSlideTimer = 0f;
        }
        else if (stopSlideTimer > 0f && isGrounded)
        {
            float newX = Mathf.MoveTowards(rb.linearVelocity.x, 0f, slideSlowdown * Time.fixedDeltaTime);
            rb.linearVelocity = new Vector2(newX, rb.linearVelocity.y);
            stopSlideTimer -= Time.fixedDeltaTime;
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
    }
}