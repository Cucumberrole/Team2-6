using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 3f;
    public float jumpPower = 5.6f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGround;
    private float speedBonus;
    private bool doubleJumpEnabled;
    private bool doubleJumpUsed;
    private int facingDirection = 1;

    private readonly HashSet<Collider2D> groundColliders = new();

    public int FacingDirection => facingDirection;
    public bool IsGround => isGround;
    public float HorizontalInput => moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveInput();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        float currentMoveSpeed = moveSpeed + speedBonus;
        rb.linearVelocity = new Vector2(moveInput * currentMoveSpeed, rb.linearVelocity.y);
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
            return;
        }

        // 1面能力取得後は空中でもう1回ジャンプ可能
        if (doubleJumpEnabled && !doubleJumpUsed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            doubleJumpUsed = true;
        }
    }

    // 1面能力
    public void ActivateSpeedDoubleJump(float minSpeedIncrease, float maxSpeedIncrease)
    {
        speedBonus = Random.Range(minSpeedIncrease, maxSpeedIncrease);
        doubleJumpEnabled = true;
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
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        UpdateGroundState(collision);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        groundColliders.Remove(collision.collider);
        isGround = groundColliders.Count > 0;
    }
}