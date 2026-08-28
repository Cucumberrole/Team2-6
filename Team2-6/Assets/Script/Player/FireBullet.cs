using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 5f;
    public int damage = 1;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Initialize(int direction)
    {
        rb.linearVelocity = Vector2.right * direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyMove enemy = other.GetComponentInParent<EnemyMove>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        BurnableObject burnable = other.GetComponentInParent<BurnableObject>();

        if (burnable != null)
        {
            burnable.Burn();
            Destroy(gameObject);
        }
    }
}