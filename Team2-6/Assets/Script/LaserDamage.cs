using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    public int damage = 1; // プレイヤーに与えるダメージ量

    void OnTriggerEnter2D(Collider2D other)
    {
        // 接触したオブジェクトからPlayerHealthを取得する
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        // PlayerHealthが見つかった場合、ダメージを与える
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}