using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("HP設定")]
    public int maxHp = 5;

    [Header("バリア表示")]
    public GameObject barrierVisual;

    private int currentHp;
    private bool isInvincible;
    private bool hasBarrier;
    private bool isDead;

    private float barrierEndTime = -1f;

    private Coroutine invincibleCoroutine;
    private Coroutine barrierCoroutine;

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
        }
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

        yield return new WaitForSeconds(duration);

        isInvincible = false;
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

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        Debug.Log("Playerが倒れました");
        SceneManager.LoadScene("GameOver");
    }
}