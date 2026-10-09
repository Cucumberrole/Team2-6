using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 3f;
    public float jumpPower = 5.6f;

    [Header("5面：突進")]
    public float dashSpeed = 12f;
    public float dashDuration = 0.3f;

    private Rigidbody2D rb;
    private PlayerSE playerSE;
    private float moveInput;
    private bool isGround;
    private bool doubleJumpEnabled;
    private bool doubleJumpUsed;
    private int facingDirection = 1;

    private bool isDashing;
    private int dashDirection;
    private Coroutine dashCoroutine;

    private readonly HashSet<Collider2D> groundColliders = new();

    public int FacingDirection => facingDirection;
    public bool IsGround => isGround;
    public bool IsDashing => isDashing;
    public float HorizontalInput => moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSE = GetComponent<PlayerSE>();
    }

    void Update()
    {
        if (isDashing)
        {
            playerSE?.SetFootsteps(false);
            return;
        }

        MoveInput();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        playerSE?.SetFootsteps(isGround && Mathf.Abs(moveInput) > 0.01f);
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, 0f);
            return;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void MoveInput()
    {
        moveInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
            facingDirection = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
            facingDirection = 1;
        }
    }

    private void Jump()
    {
        if (isGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isGround = false;
            doubleJumpUsed = false;

            playerSE?.PlayJump();
            playerSE?.SetFootsteps(false);
            return;
        }

        if (doubleJumpEnabled && !doubleJumpUsed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            doubleJumpUsed = true;

            playerSE?.PlayJump();
        }
    }

    public void ActivateDoubleJump()
    {
        doubleJumpEnabled = true;
    }

    public void ActivateDash()
    {
        if (isDashing)
        {
            return;
        }

        dashDirection = facingDirection;

        playerSE?.PlayDash();
        playerSE?.SetFootsteps(false);

        dashCoroutine = StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        isDashing = true;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        dashCoroutine = null;
    }

    private void TryBreakObject(Collider2D targetCollider)
    {
        if (!isDashing)
        {
            return;
        }

        BreakableObject breakableObject =
            targetCollider.GetComponentInParent<BreakableObject>();

        if (breakableObject != null)
        {
            breakableObject.Break();
        }
    }

    private bool IsGroundContact(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                return true;
            }
        }

        return false;
    }

    private void UpdateGroundState(Collision2D collision)
    {
        if (rb.linearVelocity.y <= 0.1f && IsGroundContact(collision))
        {
            groundColliders.Add(collision.collider);
        }
        else
        {
            groundColliders.Remove(collision.collider);
        }

        isGround = groundColliders.Count > 0;

        if (isGround)
        {
            doubleJumpUsed = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        UpdateGroundState(collision);
        TryBreakObject(collision.collider);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        UpdateGroundState(collision);
        TryBreakObject(collision.collider);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        groundColliders.Remove(collision.collider);
        isGround = groundColliders.Count > 0;
    }

    void OnDisable()
    {
        playerSE?.SetFootsteps(false);
    }
}
