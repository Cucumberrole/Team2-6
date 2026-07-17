using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("ˆÚ“®İ’è")]
    public float moveSpeed = 5f;
    public float jumpPower = 8f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGround;

    [Header("ŠƒAƒCƒeƒ€")]
    private bool hasItem;
    private float itemMinSpeedIncrease;
    private float itemMaxSpeedIncrease;
    private float itemSpeedBoostDuration;

    [Header("”­“®’†‚ÌŒø‰Ê")]
    private float speedBonus;
    private bool canDoubleJump;
    private Coroutine speedBoostCoroutine;

    private readonly HashSet<Collider2D> groundColliders = new();

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

        if (Input.GetMouseButtonDown(0) && hasItem)
        {
            ActivateItem();
        }
    }

    void FixedUpdate()
    {
        float currentMoveSpeed = moveSpeed + speedBonus;

        rb.linearVelocity = new Vector2(
            moveInput * currentMoveSpeed,
            rb.linearVelocity.y
        );
    }

    private void MoveInput()
    {
        moveInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
        }
    }

    private void Jump()
    {
        if (isGround)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpPower
            );

            isGround = false;
            return;
        }

        if (canDoubleJump)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpPower
            );

            canDoubleJump = false;
        }
    }

    public void GetItem(
        float minSpeedIncrease,
        float maxSpeedIncrease,
        float speedBoostDuration
    )
    {
        hasItem = true;

        itemMinSpeedIncrease = minSpeedIncrease;
        itemMaxSpeedIncrease = maxSpeedIncrease;
        itemSpeedBoostDuration = speedBoostDuration;
    }

    private void ActivateItem()
    {
        hasItem = false;

        float randomSpeedBonus = Random.Range(
            itemMinSpeedIncrease,
            itemMaxSpeedIncrease
        );

        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
        }

        speedBoostCoroutine = StartCoroutine(
            SpeedBoostRoutine(
                randomSpeedBonus,
                itemSpeedBoostDuration
            )
        );

        canDoubleJump = true;
    }

    private IEnumerator SpeedBoostRoutine(
        float bonus,
        float duration
    )
    {
        speedBonus = bonus;

        yield return new WaitForSeconds(duration);

        speedBonus = 0f;
        speedBoostCoroutine = null;
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
        if (rb.linearVelocity.y <= 0.1f &&
            IsGroundContact(collision))
        {
            groundColliders.Add(collision.collider);
        }
        else
        {
            groundColliders.Remove(collision.collider);
        }

        isGround = groundColliders.Count > 0;
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