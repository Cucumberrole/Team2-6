using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    public int damage = 1; // プレイヤーに与えるダメージ量

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}