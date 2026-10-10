using System.Collections;
using UnityEngine;
using System.Collections.Generic;

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
    [Min(0f)] public float invincibleCooldown = 0.5f;

    [Header("3面：凍結")]
    public float freezeDuration = 3f;
    public float tileSize = 1f;
    public float freezeOffsetX = 1f;
    public float freezeOffsetY = 0f;
    [Min(0f)] public float freezeCooldown = 0.5f;

    [Header("3面：凍結エフェクト")]
    public GameObject freezeEffectObject;
    public float freezeEffectDuration = 0.3f;

    [Header("4面：バリア")]
    [Min(0f)] public float barrierCooldown = 0.5f;

    [Header("5面：突進")]
    [Min(0f)] public float dashCooldown = 0.7f;

    [Header("6面：追尾弾")]
    public Transform firePoint;
    public GameObject homingBulletPrefab;
    [Min(0f)] public float homingShotCooldown = 0.6f;

    [Header("7面：火炎弾")]
    public GameObject fireBulletPrefab;
    [Min(0f)] public float fireShotCooldown = 0.6f;

    private PlayerMove playerMove;
    private PlayerHealth playerHealth;
    private PlayerSE playerSE;

    private Coroutine freezeEffectCoroutine;

    private HashSet<PlayerAbilityType> unlockedAbilities =
    new HashSet<PlayerAbilityType>();

    private bool invincibleAbilityLocked;
    private bool freezeAbilityLocked;
    private bool barrierAbilityLocked;
    private bool dashAbilityLocked;
    private bool homingShotAbilityLocked;
    private bool fireShotAbilityLocked;

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
        if (HasAbility(PlayerAbilityType.DoubleJump)
        && Input.GetKeyDown(KeyCode.H))
        {
            ActivateAbility(PlayerAbilityType.DoubleJump);
        }

        if (HasAbility(PlayerAbilityType.Invincible)
            && Input.GetKeyDown(KeyCode.J))
        {
            ActivateAbility(PlayerAbilityType.Invincible);
        }

        if (HasAbility(PlayerAbilityType.Freeze)
            && Input.GetKeyDown(KeyCode.K))
        {
            ActivateAbility(PlayerAbilityType.Freeze);
        }

        if (HasAbility(PlayerAbilityType.BarrierOneHit)
            && Input.GetKeyDown(KeyCode.L))
        {
            ActivateAbility(PlayerAbilityType.BarrierOneHit);
        }

        if (HasAbility(PlayerAbilityType.Dash)
            && Input.GetKeyDown(KeyCode.N))
        {
            ActivateAbility(PlayerAbilityType.Dash);
        }

        if (HasAbility(PlayerAbilityType.HomingShot)
            && Input.GetKeyDown(KeyCode.M))
        {
            ActivateAbility(PlayerAbilityType.HomingShot);
        }

        if (HasAbility(PlayerAbilityType.FireShot)
            && Input.GetKeyDown(KeyCode.Comma))
        {
            ActivateAbility(PlayerAbilityType.FireShot);
        }
    }

    public void AcquireAbility(PlayerAbilityType abilityType)
    {
        unlockedAbilities.Add(abilityType);
        currentAbility = abilityType;
        Debug.Log("能力取得：" + abilityType);
    }

    public bool HasAbility(PlayerAbilityType abilityType)
    {
        return unlockedAbilities.Contains(abilityType);
    }

    private void ActivateAbility(PlayerAbilityType abilityType)
    {
        switch (abilityType)
        {
            case PlayerAbilityType.DoubleJump:
                if (playerMove == null)
                {
                    return;
                }

                playerMove.ActivateDoubleJump();
                playerSE?.PlayGenericAbility();
                break;

            case PlayerAbilityType.Invincible:
                ActivateInvincibleAbility();
                break;

            case PlayerAbilityType.Freeze:
                ActivateFreezeAbility();
                break;

            case PlayerAbilityType.BarrierOneHit:
                ActivateBarrierAbility();
                break;

            case PlayerAbilityType.Dash:
                ActivateDashAbility();
                break;

            case PlayerAbilityType.HomingShot:
                ActivateHomingShotAbility();
                break;

            case PlayerAbilityType.FireShot:
                ActivateFireShotAbility();
                break;
        }
    }

    private void ActivateInvincibleAbility()
    {
        if (invincibleAbilityLocked || playerHealth == null)
        {
            return;
        }

        invincibleAbilityLocked = true;
        playerHealth.ActivateInvincible(invincibleDuration);
        playerSE?.PlayGenericAbility();

        StartCoroutine(InvincibleAbilityRoutine());
    }

    private IEnumerator InvincibleAbilityRoutine()
    {
        while (playerHealth != null && playerHealth.IsInvincible)
        {
            yield return null;
        }

        if (invincibleCooldown > 0f)
        {
            yield return new WaitForSeconds(invincibleCooldown);
        }

        invincibleAbilityLocked = false;
    }

    private void ActivateFreezeAbility()
    {
        if (freezeAbilityLocked || playerMove == null)
        {
            return;
        }

        freezeAbilityLocked = true;
        ActivateFreeze();
        playerSE?.PlayFreeze();

        StartCoroutine(FreezeAbilityRoutine());
    }

    private IEnumerator FreezeAbilityRoutine()
    {
        if (freezeDuration > 0f)
        {
            yield return new WaitForSeconds(freezeDuration);
        }

        if (freezeCooldown > 0f)
        {
            yield return new WaitForSeconds(freezeCooldown);
        }

        freezeAbilityLocked = false;
    }

    private void ActivateBarrierAbility()
    {
        if (barrierAbilityLocked || playerHealth == null)
        {
            return;
        }

        barrierAbilityLocked = true;
        playerHealth.ActivateBarrier(0f);
        playerSE?.PlayGenericAbility();

        StartCoroutine(BarrierAbilityRoutine());
    }

    private IEnumerator BarrierAbilityRoutine()
    {
        while (playerHealth != null && playerHealth.HasBarrier)
        {
            yield return null;
        }

        if (barrierCooldown > 0f)
        {
            yield return new WaitForSeconds(barrierCooldown);
        }

        barrierAbilityLocked = false;
    }

    private void ActivateDashAbility()
    {
        if (dashAbilityLocked || playerMove == null || playerMove.IsDashing)
        {
            return;
        }

        dashAbilityLocked = true;
        playerMove.ActivateDash();

        StartCoroutine(DashAbilityRoutine());
    }

    private IEnumerator DashAbilityRoutine()
    {
        while (playerMove != null && playerMove.IsDashing)
        {
            yield return null;
        }

        if (dashCooldown > 0f)
        {
            yield return new WaitForSeconds(dashCooldown);
        }

        dashAbilityLocked = false;
    }

    private void ActivateHomingShotAbility()
    {
        if (homingShotAbilityLocked)
        {
            return;
        }

        if (!ShootHomingBullet())
        {
            return;
        }

        homingShotAbilityLocked = true;
        StartCoroutine(HomingShotCooldownRoutine());
    }

    private IEnumerator HomingShotCooldownRoutine()
    {
        if (homingShotCooldown > 0f)
        {
            yield return new WaitForSeconds(homingShotCooldown);
        }

        homingShotAbilityLocked = false;
    }

    private void ActivateFireShotAbility()
    {
        if (fireShotAbilityLocked)
        {
            return;
        }

        if (!ShootFireBullet())
        {
            return;
        }

        fireShotAbilityLocked = true;
        StartCoroutine(FireShotCooldownRoutine());
    }

    private IEnumerator FireShotCooldownRoutine()
    {
        if (fireShotCooldown > 0f)
        {
            yield return new WaitForSeconds(fireShotCooldown);
        }

        fireShotAbilityLocked = false;
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

    private bool ShootHomingBullet()
    {
        if (firePoint == null || homingBulletPrefab == null)
        {
            return false;
        }

        Instantiate(homingBulletPrefab, firePoint.position, Quaternion.identity);
        playerSE?.PlayShot();

        return true;
    }

    private bool ShootFireBullet()
    {
        if (firePoint == null || fireBulletPrefab == null)
        {
            return false;
        }

        GameObject bullet = Instantiate(
            fireBulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        FireBullet fireBullet = bullet.GetComponent<FireBullet>();

        if (fireBullet != null && playerMove != null)
        {
            fireBullet.Initialize(playerMove.FacingDirection);
        }

        playerSE?.PlayFireShot();

        return true;
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
