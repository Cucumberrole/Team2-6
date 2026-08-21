using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [Header("付与する能力")]
    public PlayerAbilityType abilityType;

    [Header("速度上昇能力用")]
    public float minSpeedIncrease = 1f;
    public float maxSpeedIncrease = 2f;
    public float speedBoostDuration = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerAbility playerAbility = other.GetComponentInParent<PlayerAbility>();

        if (playerAbility == null)
        {
            return;
        }

        playerAbility.AcquireAbility(abilityType, minSpeedIncrease, maxSpeedIncrease, speedBoostDuration);
        Destroy(gameObject);
    }
}
