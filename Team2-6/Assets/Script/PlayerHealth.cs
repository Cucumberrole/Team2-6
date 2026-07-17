using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("HP設定")]
    public int maxHp = 5;

    private int currentHp;
    private bool isInvincible;

    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        // 無敵中はダメージを受けない
        if (isInvincible)
        {
            return;
        }

        currentHp -= damage;
        Debug.Log("現在のHP：" + currentHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void SetInvincible(bool value)
    {
        isInvincible = value;
    }

    private void Die()
    {
        Debug.Log("Playerが倒れました");
    }
}