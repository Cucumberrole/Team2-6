using UnityEngine;

public enum PlayerAbilityType
{
    None,
    SpeedDoubleJump,
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

    [Header("凍結能力")]
    public float freezeDuration = 3f;
    public float tileSize = 1f;
    public Vector2 freezeAreaOffset = Vector2.zero;

    [Header("3秒バリア")]
    public float timedBarrierDuration = 3f;

    [Header("弾")]
    public Transform firePoint;
    public GameObject homingBulletPrefab;
    public GameObject fireBulletPrefab;

    private PlayerMove playerMove;
    private PlayerHealth playerHealth;
    private float speedMinIncrease = 1f;
    private float speedMaxIncrease = 2f;
    private float speedDuration = 5f;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentAbility != PlayerAbilityType.None)
        {
            ActivateAbility();
        }
    }

    public void AcquireAbility(PlayerAbilityType abilityType, float minSpeedIncrease = 1f, float maxSpeedIncrease = 2f, float speedBoostDuration = 5f)
    {
        currentAbility = abilityType;
        speedMinIncrease = minSpeedIncrease;
        speedMaxIncrease = maxSpeedIncrease;
        speedDuration = speedBoostDuration;
    }

    private void ActivateAbility()
    {
        switch (currentAbility)
        {
            case PlayerAbilityType.SpeedDoubleJump:
                playerMove.ActivateSpeedDoubleJump(speedMinIncrease, speedMaxIncrease, speedDuration);
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

    // Playerを中心に縦3マス分を凍結
    private void ActivateFreeze()
    {
        Vector2 center = (Vector2)transform.position + freezeAreaOffset;
        Vector2 size = new Vector2(tileSize, tileSize * 3f);
        Collider2D[] hits = Physics2D.OverlapBoxAll(center, size, 0f);

        foreach (Collider2D hit in hits)
        {
            EnemyFreeze enemyFreeze = hit.GetComponentInParent<EnemyFreeze>();
            if (enemyFreeze != null)
            {
                enemyFreeze.Freeze(freezeDuration);
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
        if (homingBulletPrefab == null || firePoint == null)
        {
            return;
        }

        Instantiate(homingBulletPrefab, firePoint.position, Quaternion.identity);
    }

    private void ShootFireBullet()
    {
        if (fireBulletPrefab == null || firePoint == null)
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
        Gizmos.DrawWireCube((Vector2)transform.position + freezeAreaOffset, new Vector2(tileSize, tileSize * 3f));
    }
}
