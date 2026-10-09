using System.Collections;
using UnityEngine;

public class PlayerDamageFlash : MonoBehaviour
{
    [Header("ì_ñ≈Ç≥ÇπÇÈâÊëú")]
    public SpriteRenderer spriteRenderer;

    [Header("ì_ñ≈ê›íË")]
    public int flashCount = 3;
    public float flashInterval = 0.1f;
    public Color flashColor = Color.red;

    private Coroutine flashCoroutine;

    public void Flash()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        Color originalColor = spriteRenderer.color;

        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = flashColor;

            yield return new WaitForSeconds(flashInterval);

            spriteRenderer.color = originalColor;

            yield return new WaitForSeconds(flashInterval);
        }

        spriteRenderer.color = originalColor;
        flashCoroutine = null;
    }
}