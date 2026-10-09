using System.Collections;
using UnityEngine;

public class FreezableWater : MonoBehaviour
{
    [Header("水面のCollider")]
    public Collider2D waterCollider;

    [Header("水面と氷の画像")]
    public SpriteRenderer waterSpriteRenderer;
    public Sprite frozenSprite;

    private bool originalIsTrigger;
    private Sprite originalSprite;
    private Coroutine freezeCoroutine;

    void Awake()
    {
        if (waterCollider == null)
        {
            waterCollider = GetComponent<Collider2D>();
        }

        if (waterSpriteRenderer == null)
        {
            waterSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (waterCollider != null)
        {
            originalIsTrigger = waterCollider.isTrigger;
        }

        if (waterSpriteRenderer != null)
        {
            originalSprite = waterSpriteRenderer.sprite;
        }
    }

    public void Freeze(float duration)
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
        }

        freezeCoroutine = StartCoroutine(FreezeRoutine(Mathf.Max(0f, duration)));
    }

    private IEnumerator FreezeRoutine(float duration)
    {
        if (waterSpriteRenderer != null && frozenSprite != null)
        {
            waterSpriteRenderer.sprite = frozenSprite;
        }

        if (waterCollider != null)
        {
            waterCollider.isTrigger = false;
        }

        yield return new WaitForSeconds(duration);

        RestoreWater();
        freezeCoroutine = null;
    }

    private void RestoreWater()
    {
        if (waterCollider != null)
        {
            waterCollider.isTrigger = originalIsTrigger;
        }

        if (waterSpriteRenderer != null)
        {
            waterSpriteRenderer.sprite = originalSprite;
        }
    }

    void OnDisable()
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
            freezeCoroutine = null;
        }

        RestoreWater();
    }
}
