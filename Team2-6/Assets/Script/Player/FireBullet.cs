using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 5f;
    public int damage = 1;

    private int direction = 1;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Initialize(int facingDirection)
    {
        direction = facingDirection;
    }

    void Update()
    {
        transform.position += Vector3.right * direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();

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
