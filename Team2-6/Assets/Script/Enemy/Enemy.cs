using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public static int EnemyHP = 5;
    public static int PlayerHP = 5;
    public float speed = 2f;

    public Transform groundCheck;
    public float checkDistance = 0.3f;

    public GameObject ground;

    private bool canFlip = true;

    void Update()
    {
        // 横移動
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // 下方向にRayを飛ばす
        RaycastHit2D hit = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            checkDistance
        );

        bool isOnSpecifiedGround =
            hit.collider != null &&
            hit.collider.gameObject == ground;

        // 指定した地面が無くなったら反転
        if (!isOnSpecifiedGround && canFlip)
        {
            Flip();
            canFlip = false;
        }

        // 再び地面を感知したら、次の反転を許可
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

        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            groundCheck.position,
            groundCheck.position + Vector3.down * checkDistance
        );
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("プレイヤーに当たった！");
            PlayerHP -= 1;
        }
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("弾に当たった！");
            EnemyHP -= 1;
        }
    }
  
}