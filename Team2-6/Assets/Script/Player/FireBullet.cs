using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 5f;

    private Rigidbody2D rb;
    private bool hasHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Initialize(int direction)
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.right * direction * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit)
        {
            return;
        }

        EnemyBurn enemy = other.GetComponentInParent<EnemyBurn>();
        if (enemy != null)
        {
            hasHit = true;
            enemy.Burn();
            Destroy(gameObject);
            return;
        }

        BurnableObject tree = other.GetComponentInParent<BurnableObject>();
        if (tree != null)
        {
            hasHit = true;
            tree.Burn();
            Destroy(gameObject);
        }
    }
}
