using UnityEngine;

public enum PlayerAbilityType
{
    None,
    DoubleJump,
    Invincible,
    Freeze,
    BarrierOneHit,
    BarrierTimed,
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
    public Vector2 freezeAreaOffset;

    [Header("5面：時間制バリア")]
    public float timedBarrierDuration = 3f;

    [Header("6・7面：弾")]
    public Transform firePoint;
    public GameObject homingBulletPrefab;
    public GameObject fireBulletPrefab;

    private PlayerMove playerMove;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerHealth = GetComponent<PlayerHealth>();
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

            case PlayerAbilityType.BarrierTimed:
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
                break;

            case PlayerAbilityType.Invincible:
                playerHealth.ActivateInvincible(invincibleDuration);
                break;

            case PlayerAbilityType.Freeze:
                ActivateFreeze();
                break;

            case PlayerAbilityType.BarrierOneHit:
                playerHealth.ActivateBarrier(0f);
                break;

            case PlayerAbilityType.BarrierTimed:
                playerHealth.ActivateBarrier(timedBarrierDuration);
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
        Vector2 center = (Vector2)transform.position + freezeAreaOffset;
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

    private void ShootHomingBullet()
    {
        if (firePoint == null || homingBulletPrefab == null)
        {
            return;
        }

        Instantiate(homingBulletPrefab, firePoint.position, Quaternion.identity);
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
    }

    void OnDrawGizmosSelected()
    {
        Vector2 center = (Vector2)transform.position + freezeAreaOffset;
        Gizmos.DrawWireCube(center, new Vector2(tileSize, tileSize * 3f));
    }
}