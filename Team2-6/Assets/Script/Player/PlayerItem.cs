using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [Header("取得できる能力")]
    public PlayerAbilityType abilityType;

    [Header("表示")]
    public SpriteRenderer spriteRenderer;

    [Header("能力ごとの画像")]
    public Sprite doubleJumpSprite;
    public Sprite invincibleSprite;
    public Sprite freezeSprite;
    public Sprite barrierOneHitSprite;
    public Sprite dashSprite;
    public Sprite homingShotSprite;
    public Sprite fireShotSprite;

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
        GameAudioManager.Instance?.PlayItemGet();
        Destroy(gameObject);
    }

    void OnValidate()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        switch (abilityType)
        {
            case PlayerAbilityType.DoubleJump:
                spriteRenderer.sprite = doubleJumpSprite;
                break;

            case PlayerAbilityType.Invincible:
                spriteRenderer.sprite = invincibleSprite;
                break;

            case PlayerAbilityType.Freeze:
                spriteRenderer.sprite = freezeSprite;
                break;

            case PlayerAbilityType.BarrierOneHit:
                spriteRenderer.sprite = barrierOneHitSprite;
                break;

            case PlayerAbilityType.Dash:
                spriteRenderer.sprite = dashSprite;
                break;

            case PlayerAbilityType.HomingShot:
                spriteRenderer.sprite = homingShotSprite;
                break;

            case PlayerAbilityType.FireShot:
                spriteRenderer.sprite = fireShotSprite;
                break;
        }
    }
}