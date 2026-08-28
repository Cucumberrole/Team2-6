using UnityEngine;

public class AbilityItem : MonoBehaviour
{
    [Header("Žæ“¾‚Å‚«‚é”\—Í")]
    public PlayerAbilityType abilityType;

    private bool collected;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected || !other.CompareTag("Player"))
        {
            return;
        }

        PlayerAbility playerAbility = other.GetComponentInParent<PlayerAbility>();

        if (playerAbility == null)
        {
            return;
        }

        collected = true;
        playerAbility.AcquireAbility(abilityType);
        Destroy(gameObject);
    }
}