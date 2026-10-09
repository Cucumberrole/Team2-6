using System.Collections;
using UnityEngine;

public class EnemyFreeze : MonoBehaviour
{
    [Header("凍結中に停止させるスクリプト")]
    public MonoBehaviour[] scriptsToDisable;

    [Header("凍結表示")]
    public GameObject frozenVisual;
    public SpriteRenderer enemySpriteRenderer;
    public Color frozenColor = new Color(0.6f, 0.85f, 1f, 1f);

    private Rigidbody2D rb;
    private Animator animator;
    private RigidbodyConstraints2D originalConstraints;
    private Color originalColor;
    private Coroutine freezeCoroutine;
    private bool isFrozen;
    private bool isBurning;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (enemySpriteRenderer == null)
        {
            enemySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if (enemySpriteRenderer != null)
        {
            originalColor = enemySpriteRenderer.color;
        }

        if (rb != null)
        {
            originalConstraints = rb.constraints;
        }

        if (frozenVisual != null)
        {
            frozenVisual.SetActive(false);
        }
    }

    public void Freeze(float duration)
    {
        if (isBurning)
        {
            return;
        }

        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
        }

        freezeCoroutine = StartCoroutine(FreezeRoutine(duration));
    }

    public void StopForBurning()
    {
        isBurning = true;
        isFrozen = false;

        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
            freezeCoroutine = null;
        }

        if (frozenVisual != null)
        {
            frozenVisual.SetActive(false);
        }

        if (enemySpriteRenderer != null)
        {
            enemySpriteRenderer.color = originalColor;
        }
    }

    private IEnumerator FreezeRoutine(float duration)
    {
        isFrozen = true;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        foreach (MonoBehaviour script in scriptsToDisable)
        {
            if (script != null)
            {
                script.enabled = false;
            }
        }

        if (animator != null)
        {
            animator.speed = 0f;
        }

        if (frozenVisual != null)
        {
            frozenVisual.SetActive(true);
        }

        if (enemySpriteRenderer != null)
        {
            enemySpriteRenderer.color = frozenColor;
        }

        yield return new WaitForSeconds(duration);

        Unfreeze();
    }

    private void Unfreeze()
    {
        isFrozen = false;

        if (rb != null)
        {
            rb.constraints = originalConstraints;
        }

        foreach (MonoBehaviour script in scriptsToDisable)
        {
            if (script != null)
            {
                script.enabled = true;
            }
        }

        if (animator != null)
        {
            animator.speed = 1f;
        }

        if (frozenVisual != null)
        {
            frozenVisual.SetActive(false);
        }

        if (enemySpriteRenderer != null)
        {
            enemySpriteRenderer.color = originalColor;
        }

        freezeCoroutine = null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFrozen && collision.gameObject.GetComponentInParent<PlayerMove>() != null)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isFrozen && other.GetComponentInParent<PlayerMove>() != null)
        {
            Destroy(gameObject);
        }
    }
}
