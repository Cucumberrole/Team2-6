using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [Header("弾の設定")]
    public float speed = 6f;
    public float lifeTime = 5f;
    public int damage = 1;

    [Header("敵の検知設定")]
    [Min(0f)]
    public float detectionRange = 5f;

    private EnemyMove target;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindClosestEnemy();

        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        if (rb == null)
        {
            return;
        }

        if (target != null)
        {
            float distance =
                Vector2.Distance(transform.position, target.transform.position);

            if (distance > detectionRange)
            {
                target = null;
            }
        }

        if (target == null)
        {
            target = FindClosestEnemy();

            if (target == null)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }
        }

        Vector2 direction =
            (target.transform.position - transform.position).normalized;

        rb.linearVelocity = direction * speed;
    }

    private EnemyMove FindClosestEnemy()
    {
        EnemyMove[] enemies =
            FindObjectsByType<EnemyMove>(FindObjectsSortMode.None);

        EnemyMove closest = null;
        float closestDistance = detectionRange;

        foreach (EnemyMove enemy in enemies)
        {
            if (enemy == null)
            {
                continue;
            }

            float distance =
                Vector2.Distance(transform.position, enemy.transform.position);

            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyMove enemy = other.GetComponentInParent<EnemyMove>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
