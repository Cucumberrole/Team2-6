using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int EnemyHP = 5;
    public float speed = 2f;

    public Transform groundCheck;
    public float checkDistance = 0.3f;
    public GameObject ground;

    private bool canFlip = true;

    public SpriteRenderer spriteRenderer;
    public Sprite left;
    public Sprite right;

    public float groundCheckX = 1.0f;

    void Start()
    {
        UpdateDirection();
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            checkDistance
        );

        bool isOnSpecifiedGround =
            hit.collider != null &&
            hit.collider.gameObject == ground;

        if (!isOnSpecifiedGround && canFlip)
        {
            Flip();
            canFlip = false;
        }

        if (isOnSpecifiedGround)
        {
            canFlip = true;
        }

        if (EnemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        speed *= -1f;
        UpdateDirection();
    }

    void UpdateDirection()
    {
        if (groundCheck == null || spriteRenderer == null)
        {
            return;
        }

        Vector3 pos = groundCheck.localPosition;

        if (speed > 0)
        {
            spriteRenderer.sprite = right;
            pos.x = groundCheckX;
        }
        else
        {
            spriteRenderer.sprite = left;
            pos.x = -groundCheckX;
        }

        groundCheck.localPosition = pos;
    }

    // 外部から敵にダメージを与える
    public void TakeDamage(int damage)
    {
        EnemyHP -= damage;

        Debug.Log("敵のHP：" + EnemyHP);

        if (EnemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーに当たった
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (groundCheck == null)
        {
            return;
        }

        Gizmos.color = Color.red;

        Gizmos.DrawLine(
            groundCheck.position,
            groundCheck.position + Vector3.down * checkDistance
        );
    }
}