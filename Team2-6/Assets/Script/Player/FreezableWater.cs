using System.Collections;
using UnityEngine;

public class FreezableWater : MonoBehaviour
{
    [Header("水面のCollider")]
    public Collider2D waterCollider;

    [Header("凍結表示")]
    public GameObject frozenVisual;

    private bool originalIsTrigger;
    private Coroutine freezeCoroutine;

    void Start()
    {
        if (waterCollider == null)
        {
            waterCollider = GetComponent<Collider2D>();
        }

        if (waterCollider != null)
        {
            originalIsTrigger = waterCollider.isTrigger;
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
        if (waterCollider != null)
        {
            waterCollider.isTrigger = false;
        }

        if (frozenVisual != null)
        {
            frozenVisual.SetActive(true);
        }

        yield return new WaitForSeconds(duration);

        if (waterCollider != null)
        {
            waterCollider.isTrigger = originalIsTrigger;
        }

        if (frozenVisual != null)
        {
            frozenVisual.SetActive(false);
        }

        freezeCoroutine = null;
    }
}
