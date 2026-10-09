using System.Collections;
using UnityEngine;

public enum PlayerAbilityType
{
    None,
    DoubleJump,
    Invincible,
    Freeze,
    BarrierOneHit,
    Dash,
    HomingShot,
    FireShot
}

public class PlayerAbility : MonoBehaviour
{
    [Header("現在の能力")]
    public PlayerAbilityType currentAbility = PlayerAbilityType.None;

    [Header("2面：無敵")]
    public float invincibleDuration = 3f;

    [Header("3面：凍結")]
    public float freezeDuration = 3f;
    public float tileSize = 1f;
    public float freezeOffsetX = 1f;
    public float freezeOffsetY = 0f;

    [Header("3面：凍結エフェクト")]
    public GameObject freezeEffectObject;
    public float freezeEffectDuration = 0.3f;

    [Header("6・7面：弾")]
    public Transform firePoint;
    public GameObject homingBulletPrefab;
    public GameObject fireBulletPrefab;

    private PlayerMove playerMove;
    private PlayerHealth playerHealth;
    private PlayerSE playerSE;
    private Coroutine freezeEffectCoroutine;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerHealth = GetComponent<PlayerHealth>();
        playerSE = GetComponent<PlayerSE>();

        if (freezeEffectObject != null)
        {
            freezeEffectObject.SetActive(false);
        }
    }

    void Update()
    {
        switch (currentAbility)
        {
            case PlayerAbilityType.DoubleJump:
                if (Input.GetKeyDown(KeyCode.H))
                {
                    ActivateAbility();
                }
                break;

            case PlayerAbilityType.Invincible:
                if (Input.GetKeyDown(KeyCode.J))
                {
                    ActivateAbility();
                }
                break;

            case PlayerAbilityType.Freeze:
                if (Input.GetKeyDown(KeyCode.K))
                {
                    ActivateAbility();
                }
                break;

            case PlayerAbilityType.BarrierOneHit:
                if (Input.GetKeyDown(KeyCode.L))
                {
                    ActivateAbility();
                }
                break;

            case PlayerAbilityType.Dash:
                if (Input.GetKeyDown(KeyCode.N))
                {
                    ActivateAbility();
                }
                break;

            case PlayerAbilityType.HomingShot:
                if (Input.GetKeyDown(KeyCode.M))
                {
                    ActivateAbility();
                }
                break;

            case PlayerAbilityType.FireShot:
                if (Input.GetKeyDown(KeyCode.Comma))
                {
                    ActivateAbility();
                }
                break;
        }
    }

    public void AcquireAbility(PlayerAbilityType abilityType)
    {
        currentAbility = abilityType;
        Debug.Log("能力取得：" + abilityType);
    }

    private void ActivateAbility()
    {
        switch (currentAbility)
        {
            case PlayerAbilityType.DoubleJump:
                playerMove.ActivateDoubleJump();
                playerSE?.PlayGenericAbility();
                break;

            case PlayerAbilityType.Invincible:
                playerHealth.ActivateInvincible(invincibleDuration);
                playerSE?.PlayGenericAbility();
                break;

            case PlayerAbilityType.Freeze:
                ActivateFreeze();
                playerSE?.PlayFreeze();
                break;

            case PlayerAbilityType.BarrierOneHit:
                playerHealth.ActivateBarrier(0f);
                playerSE?.PlayGenericAbility();
                break;

            case PlayerAbilityType.Dash:
                playerMove.ActivateDash();
                break;

            case PlayerAbilityType.HomingShot:
                ShootHomingBullet();
                break;

            case PlayerAbilityType.FireShot:
                ShootFireBullet();
                break;
        }
    }

    private void ActivateFreeze()
    {
        PlayFreezeEffect();

        Vector2 offset = new Vector2(
            freezeOffsetX * playerMove.FacingDirection,
            freezeOffsetY
        );

        Vector2 center = (Vector2)transform.position + offset;
        Vector2 size = new Vector2(tileSize, tileSize * 3f);
        Collider2D[] hits = Physics2D.OverlapBoxAll(center, size, 0f);

        foreach (Collider2D hit in hits)
        {
            EnemyFreeze enemy = hit.GetComponentInParent<EnemyFreeze>();

            if (enemy != null)
            {
                enemy.Freeze(freezeDuration);
            }

            FreezableWater water = hit.GetComponentInParent<FreezableWater>();

            if (water != null)
            {
                water.Freeze(freezeDuration);
            }
        }
    }

    private void PlayFreezeEffect()
    {
        if (freezeEffectObject == null)
        {
            return;
        }

        if (freezeEffectCoroutine != null)
        {
            StopCoroutine(freezeEffectCoroutine);
        }

        Vector3 rotation = freezeEffectObject.transform.localEulerAngles;

        if (playerMove.FacingDirection > 0)
        {
            rotation.y = 0f;
        }
        else
        {
            rotation.y = 180f;
        }

        freezeEffectObject.transform.localEulerAngles = rotation;
        freezeEffectCoroutine = StartCoroutine(FreezeEffectRoutine());
    }

    private IEnumerator FreezeEffectRoutine()
    {
        freezeEffectObject.SetActive(true);

        yield return new WaitForSeconds(freezeEffectDuration);

        freezeEffectObject.SetActive(false);
        freezeEffectCoroutine = null;
    }

    private void ShootHomingBullet()
    {
        if (firePoint == null || homingBulletPrefab == null)
        {
            return;
        }

        Instantiate(homingBulletPrefab, firePoint.position, Quaternion.identity);
        playerSE?.PlayShot();
    }

    private void ShootFireBullet()
    {
        if (firePoint == null || fireBulletPrefab == null)
        {
            return;
        }

        GameObject bullet = Instantiate(fireBulletPrefab, firePoint.position, Quaternion.identity);
        FireBullet fireBullet = bullet.GetComponent<FireBullet>();

        if (fireBullet != null)
        {
            fireBullet.Initialize(playerMove.FacingDirection);
        }

        playerSE?.PlayFireShot();
    }

    void OnDrawGizmosSelected()
    {
        int direction = 1;

        if (Application.isPlaying && playerMove != null)
        {
            direction = playerMove.FacingDirection;
        }

        Vector2 offset = new Vector2(
            freezeOffsetX * direction,
            freezeOffsetY
        );

        Vector2 center = (Vector2)transform.position + offset;
        Gizmos.DrawWireCube(center, new Vector2(tileSize, tileSize * 3f));
    }
}
