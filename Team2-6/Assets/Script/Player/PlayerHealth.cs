using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("HP設定")]
    public int maxHp = 5;

    [Header("バリア表示")]
    public GameObject barrierVisual;

    private int currentHp;
    private bool isInvincible;
    private bool hasBarrier;

    private Coroutine invincibleCoroutine;
    private Coroutine barrierCoroutine;

    public int CurrentHp => currentHp;

    void Start()
    {
        currentHp = maxHp;
        SetBarrierVisual(false);
    }

    public void TakeDamage(int damage)
    {
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
            barrierCoroutine = StartCoroutine(BarrierRoutine(duration));
        }
    }

    private IEnumerator BarrierRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);

        barrierCoroutine = null;
        hasBarrier = false;
        SetBarrierVisual(false);
    }

    private void RemoveBarrier()
    {
        hasBarrier = false;
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
        Debug.Log("Playerが倒れました");
    }
}