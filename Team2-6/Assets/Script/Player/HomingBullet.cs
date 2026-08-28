using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 6f;
    public float lifeTime = 5f;
    public int damage = 1;

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
        if (target == null)
        {
            target = FindClosestEnemy();
            return;
        }

        Vector2 direction = (target.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    private EnemyMove FindClosestEnemy()
    {
        EnemyMove[] enemies = FindObjectsByType<EnemyMove>(FindObjectsSortMode.None);
        EnemyMove closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (EnemyMove enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
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
}