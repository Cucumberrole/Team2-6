using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int EnemyHP = 5;
    public static int PlayerHP = 5;

    public float speed = 2f;

    public Transform groundCheck;
    public float checkDistance = 0.3f;

    public GameObject ground;

    private bool canFlip = true;

    public SpriteRenderer spriteRenderer;
    public Sprite left;
    public Sprite right;

    // GroundCheckを左右どれくらい動かすか
    public float groundCheckX = 1.0f;

    void Start()
    {
        UpdateDirection();
    }

    void Update()
    {
        // 横移動
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // GroundCheckから下にRay
        RaycastHit2D hit = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            checkDistance
        );

        bool isOnSpecifiedGround =
            hit.collider != null &&
            hit.collider.gameObject == ground;

        // 地面が無くなったら反転
        if (!isOnSpecifiedGround && canFlip)
        {
            Flip();
            canFlip = false;
        }

        // 地面を感知したら次の反転を許可
        if (isOnSpecifiedGround)
        {
            canFlip = true;
        }

        // 敵HPが0以下なら削除
        if (EnemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        // 移動方向を反転
        speed *= -1f;

        // SpriteとGroundCheckも変更
        UpdateDirection();
    }

    void UpdateDirection()
    {
        if (groundCheck == null || spriteRenderer == null)
            return;

        Vector3 pos = groundCheck.localPosition;

        if (speed > 0)
        {
            // 右向き
            spriteRenderer.sprite = right;

            // GroundCheckを右へ
            pos.x = groundCheckX;
        }
        else
        {
            // 左向き
            spriteRenderer.sprite = left;

            // GroundCheckを左へ
            pos.x = -groundCheckX;
        }

        groundCheck.localPosition = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        // プレイヤーに当たった
        if (other.CompareTag("Player"))
        {
            Debug.Log("プレイヤーに当たった！");
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(1);
            
        }

        // 弾に当たった
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("弾に当たった！");
            EnemyHP -= 1;
        }
    }

    void OnDrawGizmos()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawLine(
            groundCheck.position,
            groundCheck.position + Vector3.down * checkDistance
        );
    }
}