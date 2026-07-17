using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [Header("ë¨ìxè„è∏ó ")]
    public float minSpeedIncrease = 1f;
    public float maxSpeedIncrease = 2f;

    [Header("ë¨ìxè„è∏ÇÃåpë±éûä‘")]
    public float speedBoostDuration = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMove player =
            other.GetComponentInParent<PlayerMove>();

        if (player == null)
        {
            return;
        }

        player.GetItem(
            minSpeedIncrease,
            maxSpeedIncrease,
            speedBoostDuration
        );

        Destroy(gameObject);
    }
}