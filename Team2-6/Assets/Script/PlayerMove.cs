using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    public float jumpPower = 8f; // ジャンプの強さ

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
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

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isGround = false;
        }
    }

    void FixedUpdate()
    {
        // プレイヤーのRigidBody2Dに移動速度を与える
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}