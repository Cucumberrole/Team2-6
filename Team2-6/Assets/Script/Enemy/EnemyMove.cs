using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int EnemyHP = 5;
    public float speed = 2f;

    [Header("地面判定")]
    public Transform groundCheck;
    [Min(0f)] public float checkDistance = 0.3f;
    public LayerMask groundLayer;
    [Min(0f)] public float groundCheckX = 1f;

    [Header("向きの画像")]
    public SpriteRenderer spriteRenderer;
    public Sprite left;
    public Sprite right;

    private readonly RaycastHit2D[] groundHits = new RaycastHit2D[1];

    private void Start()
    {
        UpdateDirection();

        if (groundCheck == null || groundLayer.value == 0)
        {
            Debug.LogWarning("EnemyMove：Ground CheckとGround Layerを設定してください。", this);
        }
    }

    private void Update()
    {
        if (EnemyHP <= 0)
        {
            Destroy(gameObject);
            return;
        }

        if (groundCheck == null || groundLayer.value == 0 || speed == 0f)
        {
            return;
        }

        // 移動後の判定位置を先に調べて、端を越える前に反転する。
        float movement = speed * Time.deltaTime;
        if (!HasGroundAhead(movement))
        {
            Flip();
            movement = speed * Time.deltaTime;

            // 両側に地面がない場合は、向きを戻してその場で待つ。
            if (!HasGroundAhead(movement))
            {
                Flip();
                return;
            }
        }

        transform.Translate(Vector3.right * movement, Space.World);
    }

    private bool HasGroundAhead(float movement)
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(groundLayer);
        filter.useTriggers = false;

        Vector2 origin = (Vector2)groundCheck.position + Vector2.right * movement;
        return Physics2D.Raycast(origin, Vector2.down, filter, groundHits, checkDistance) > 0;
    }

    private void Flip()
    {
        speed = -speed;
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        bool facesRight = speed >= 0f;

        // 画像が未登録でも、判定位置の左右は更新する。
        if (groundCheck != null)
        {
            Vector3 position = groundCheck.localPosition;
            position.x = facesRight ? Mathf.Abs(groundCheckX) : -Mathf.Abs(groundCheckX);
            groundCheck.localPosition = position;
        }

        if (spriteRenderer != null)
        {
            Sprite facingSprite = facesRight ? right : left;
            if (facingSprite != null)
            {
                spriteRenderer.sprite = facingSprite;
            }
        }
    }

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
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (groundCheck == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * checkDistance);
    }
}
