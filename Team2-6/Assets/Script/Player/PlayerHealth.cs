using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("HP設定")]
    public int maxHp = 5;

    [Header("バリア表示")]
    public GameObject barrierVisual;

    [Header("無敵時の表示")]
    public SpriteRenderer playerSpriteRenderer;
    public float invincibleAlpha = 0.5f;

    private float normalAlpha = 1f;

    private int currentHp;
    private bool isInvincible;
    private bool hasBarrier;
    private bool isDead;

    private float barrierEndTime = -1f;

    private Coroutine invincibleCoroutine;
    private Coroutine barrierCoroutine;

    private PlayerDamageFlash damageFlash;
    private PlayerSE playerSE;

    public int CurrentHp => currentHp;
    public bool IsInvincible => isInvincible;
    public bool HasBarrier => hasBarrier;

    public float BarrierRemainingTime
    {
        get
        {
            if (!hasBarrier || barrierEndTime < 0f)
            {
                return -1f;
            }

            return Mathf.Max(0f, barrierEndTime - Time.time);
        }
    }

    void Start()
    {
        currentHp = maxHp;
        SetBarrierVisual(false);

        damageFlash = GetComponent<PlayerDamageFlash>();
        playerSE = GetComponent<PlayerSE>();

        if (playerSpriteRenderer == null)
        {
            playerSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (playerSpriteRenderer != null)
        {
            normalAlpha = playerSpriteRenderer.color.a;
        }
    }

    void Update()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPosition.y < -0.1f)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        if (isInvincible)
        {
            return;
        }

        if (hasBarrier)
        {
            RemoveBarrier();
            return;
        }

        currentHp -= damage;
        Debug.Log("現在のHP：" + currentHp);

        if (currentHp <= 0)
        {
            Die();
            return;
        }

        if (damageFlash != null)
        {
            damageFlash.Flash();
        }

        playerSE?.PlayDamage();
    }

    public void ActivateInvincible(float duration)
    {
        if (invincibleCoroutine != null)
        {
            StopCoroutine(invincibleCoroutine);
        }

        invincibleCoroutine = StartCoroutine(InvincibleRoutine(duration));
    }

    private IEnumerator InvincibleRoutine(float duration)
    {
        isInvincible = true;
        SetInvincibleVisual(true);

        yield return new WaitForSeconds(duration);

        isInvincible = false;
        SetInvincibleVisual(false);
        invincibleCoroutine = null;
    }

    public void ActivateBarrier(float duration)
    {
        if (barrierCoroutine != null)
        {
            StopCoroutine(barrierCoroutine);
            barrierCoroutine = null;
        }

        hasBarrier = true;
        SetBarrierVisual(true);

        if (duration > 0f)
        {
            barrierEndTime = Time.time + duration;
            barrierCoroutine = StartCoroutine(BarrierRoutine(duration));
        }
        else
        {
            barrierEndTime = -1f;
        }
    }

    private IEnumerator BarrierRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);

        hasBarrier = false;
        barrierEndTime = -1f;
        barrierCoroutine = null;

        SetBarrierVisual(false);
    }

    private void RemoveBarrier()
    {
        hasBarrier = false;
        barrierEndTime = -1f;

        SetBarrierVisual(false);

        if (barrierCoroutine != null)
        {
            StopCoroutine(barrierCoroutine);
            barrierCoroutine = null;
        }
    }

    private void SetBarrierVisual(bool value)
    {
        if (barrierVisual != null)
        {
            barrierVisual.SetActive(value);
        }
    }

    private void SetInvincibleVisual(bool value)
    {
        if (playerSpriteRenderer == null)
        {
            return;
        }

        Color color = playerSpriteRenderer.color;

        if (value)
        {
            color.a = invincibleAlpha;
        }
        else
        {
            color.a = normalAlpha;
        }

        playerSpriteRenderer.color = color;
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        Debug.Log("Playerが倒れました");

        playerSE?.SetFootsteps(false);
        playerSE?.PlayDeath();

        PlayerMove playerMove = GetComponent<PlayerMove>();

        if (playerMove != null)
        {
            playerMove.enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        float waitTime = playerSE != null ? playerSE.DeathClipLength : 0f;

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene("GameOver");
    }
}
