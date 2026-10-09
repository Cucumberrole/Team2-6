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

    [Header("壁判定")]
    public Transform wallCheck;
    [Min(0f)] public float wallCheckDistance = 0.3f;
    public LayerMask wallLayer;

    [Header("向きの画像")]
    public SpriteRenderer spriteRenderer;
    public Sprite left;
    public Sprite right;

    [Header("弾の設定")]
    public string bulletTag = "Bullet";
    public int bulletDamage = 1;

    private readonly RaycastHit2D[] groundHits =
        new RaycastHit2D[1];

    private bool isDead = false;

    private void Start()
    {
        UpdateDirection();
    }

    private void Update()
    {
        if (EnemyHP <= 0)
        {
            Die();
            return;
        }

        if (groundCheck == null ||
            groundLayer.value == 0 ||
            speed == 0f)
        {
            return;
        }

        float movement = speed * Time.deltaTime;

        if (HasWallAhead(movement) ||
            !HasGroundAhead(movement))
        {
            Flip();
            movement = speed * Time.deltaTime;

            if (HasWallAhead(movement) ||
                !HasGroundAhead(movement))
            {
                Flip();
                return;
            }
        }

        transform.Translate(
            Vector3.right * movement,
            Space.World
        );
    }

    private bool HasGroundAhead(float movement)
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(groundLayer);
        filter.useTriggers = false;

        Vector2 origin =
            (Vector2)groundCheck.position +
            Vector2.right * movement;

        return Physics2D.Raycast(
            origin,
            Vector2.down,
            filter,
            groundHits,
            checkDistance
        ) > 0;
    }

    private bool HasWallAhead(float movement)
    {
        if (wallCheck == null || wallLayer.value == 0)
        {
            return false;
        }

        Vector2 direction = speed >= 0f
            ? Vector2.right
            : Vector2.left;

        float distance =
            wallCheckDistance + Mathf.Abs(movement);

        return Physics2D.Raycast(
            wallCheck.position,
            direction,
            distance,
            wallLayer
        ).collider != null;
    }

    private void Flip()
    {
        speed = -speed;
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        bool facesRight = speed >= 0f;

        if (groundCheck != null)
        {
            Vector3 position = groundCheck.localPosition;

            position.x = facesRight
                ? Mathf.Abs(groundCheckX)
                : -Mathf.Abs(groundCheckX);

            groundCheck.localPosition = position;
        }

        if (wallCheck != null)
        {
            Vector3 position = wallCheck.localPosition;

            position.x = facesRight
                ? Mathf.Abs(position.x)
                : -Mathf.Abs(position.x);

            wallCheck.localPosition = position;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(bulletTag))
        {
            TakeDamage(bulletDamage);
            Destroy(other.gameObject);
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        EnemyFreeze enemyFreeze =
            GetComponentInParent<EnemyFreeze>();

        if (enemyFreeze != null)
        {
            if (enemyFreeze.TryHandleFrozenPlayerContact(other))
            {
                return;
            }

            if (enemyFreeze.IsFrozen)
            {
                return;
            }
        }

        PlayerHealth playerHealth =
            other.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        EnemyHP -= damage;

        Debug.Log("敵のHP：" + EnemyHP);

        if (EnemyHP <= 0)
        {
            Die();
        }
    }

    public void Defeat()
    {
        Die();
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        EnemyKey enemyKey = GetComponent<EnemyKey>();

        if (enemyKey != null)
        {
            enemyKey.DropKey();
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(
                groundCheck.position,
                groundCheck.position +
                Vector3.down * checkDistance
            );
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;

            Vector3 direction = speed >= 0f
                ? Vector3.right
                : Vector3.left;

            Gizmos.DrawLine(
                wallCheck.position,
                wallCheck.position +
                direction * wallCheckDistance
            );
        }
    }
}
