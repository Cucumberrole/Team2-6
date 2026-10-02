using System.Collections;
using UnityEngine;

public class FreezableWater : MonoBehaviour
{
    [Header("水面のCollider")]
    public Collider2D waterCollider;

    [Header("水面と氷の画像")]
    public SpriteRenderer waterSpriteRenderer;
    public Sprite frozenSprite;

    [Header("水面の子に配置した氷の息")]
    public GameObject freezeEffect;
    [Min(0f)]
    public float effectDuration = 0.5f;

    private bool originalIsTrigger;
    private Sprite originalSprite;
    private Coroutine freezeCoroutine;

    private void Awake()
    {
        if (waterCollider == null)
        {
            waterCollider = GetComponent<Collider2D>();
        }

        if (waterSpriteRenderer == null)
        {
            waterSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        // 凍結前の画像と当たり判定を保存する。
        if (waterCollider != null)
        {
            originalIsTrigger = waterCollider.isTrigger;
        }

        if (waterSpriteRenderer != null)
        {
            originalSprite = waterSpriteRenderer.sprite;
        }

        if (freezeEffect != null)
        {
            freezeEffect.SetActive(false);
        }
    }

    public void Freeze(float duration)
    {
        // 再発動した場合は、その時点から凍結時間を数え直す。
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
        }

        freezeCoroutine = StartCoroutine(FreezeRoutine(Mathf.Max(0f, duration)));
    }

    private IEnumerator FreezeRoutine(float duration)
    {
        if (freezeEffect != null)
        {
            freezeEffect.SetActive(true);
        }

        if (waterSpriteRenderer != null && frozenSprite != null)
        {
            waterSpriteRenderer.sprite = frozenSprite;
        }

        if (waterCollider != null)
        {
            waterCollider.isTrigger = false;
        }

        // 息の表示時間も凍結時間に含める。3秒の効果は合計3秒。
        float visibleTime = Mathf.Clamp(effectDuration, 0f, duration);
        if (visibleTime > 0f)
        {
            yield return new WaitForSeconds(visibleTime);
        }

        if (freezeEffect != null)
        {
            freezeEffect.SetActive(false);
        }

        yield return new WaitForSeconds(duration - visibleTime);
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

        if (freezeEffect != null)
        {
            freezeEffect.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
            freezeCoroutine = null;
        }

        RestoreWater();
    }
}
