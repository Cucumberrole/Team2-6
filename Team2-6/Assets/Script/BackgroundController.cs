using System.Collections;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public SpriteRenderer colorBackground;
    public float restoreDuration = 3f;

    void Start()
    {
        Color color = colorBackground.color;
        color.a = 0f;
        colorBackground.color = color;
    }

    public void RestoreColor()
    {
        StartCoroutine(RestoreColorRoutine());
    }

    private IEnumerator RestoreColorRoutine()
    {
        float time = 0f;

        while (time < restoreDuration)
        {
            time += Time.deltaTime;

            Color color = colorBackground.color;
            color.a = Mathf.Clamp01(time / restoreDuration);
            colorBackground.color = color;

            yield return null;
        }
    }
}