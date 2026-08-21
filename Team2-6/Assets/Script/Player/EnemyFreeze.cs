using System.Collections;
using UnityEngine;

public class EnemyFreeze : MonoBehaviour
{
    [Header("凍結中に停止させるスクリプト")]
    public MonoBehaviour[] scriptsToDisable;

    [Header("凍結表示")]
    public GameObject frozenVisual;

    private Rigidbody2D rb;
    private Animator animator;
    private RigidbodyConstraints2D originalConstraints;
    private Coroutine freezeCoroutine;
    private bool isFrozen;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
        }

        freezeCoroutine = StartCoroutine(FreezeRoutine(duration));
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
