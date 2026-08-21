//using UnityEngine;

//public class Key : MonoBehaviour
//{
//    public static bool hasKey = false;

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            hasKey = true;
//            Destroy(gameObject);
//        }
//    }
//}

using UnityEngine;

public class Key : MonoBehaviour
{
    [Header("能力を付与するか")]
    public bool grantsAbility;

    [Header("付与する能力")]
    public PlayerAbilityType abilityType = PlayerAbilityType.None;

    [Header("速度上昇能力用")]
    public float minSpeedIncrease = 1f;
    public float maxSpeedIncrease = 2f;
    public float speedBoostDuration = 5f;

    private bool collected;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected || !other.CompareTag("Player"))
        {
            return;
        }

        collected = true;
        KeyManager.Instance.CollectKey();

        // 能力付きの鍵ならPlayerに能力を付与
        if (grantsAbility)
        {
            PlayerAbility playerAbility = other.GetComponentInParent<PlayerAbility>();

            if (playerAbility != null)
            {
                playerAbility.AcquireAbility(
                    abilityType,
                    minSpeedIncrease,
                    maxSpeedIncrease,
                    speedBoostDuration
                );
            }
        }

        Destroy(gameObject);
    }
}