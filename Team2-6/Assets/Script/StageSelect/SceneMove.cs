using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    [Header("フェード用の黒いパネル")]
    public CanvasGroup fadeCanvasGroup;

    [Header("遷移先のシーン")]
    public string FastSceneName = "Tutorial";
    public string SecondSceneName = "StageSelect";

    [Header("フェード時間")]
    [Min(0f)]
    public float fadeDuration = 0.5f;

    private bool isTransitioning;
    private string targetSceneName;

    private static bool tutorialPlayed = false;

    private void Awake()
    {
        if (fadeCanvasGroup == null)
        {
            Debug.LogError("Fade Canvas Groupが設定されていません。");
            return;
        }

        isTransitioning = true;

        fadeCanvasGroup.alpha = 1f;
        fadeCanvasGroup.blocksRaycasts = true;
        fadeCanvasGroup.interactable = false;
    }

    private IEnumerator Start()
    {
        if (fadeCanvasGroup == null)
        {
            yield break;
        }

        yield return FadeTo(0f);

        fadeCanvasGroup.blocksRaycasts = false;
        isTransitioning = false;
    }

    public void ChangeScene()
    {
        if (isTransitioning || fadeCanvasGroup == null)
        {
            return;
        }

        if (!tutorialPlayed)
        {
            targetSceneName = FastSceneName;
            tutorialPlayed = true;
        }
        else
        {
            targetSceneName = SecondSceneName;
        }

        if (!Application.CanStreamedLevelBeLoaded(targetSceneName))
        {
            Debug.LogError("シーン「" + targetSceneName + "」がScene Listにありません。");
            return;
        }

        GameAudioManager.Instance?.PlayDecide();

        isTransitioning = true;
        fadeCanvasGroup.blocksRaycasts = true;

        StartCoroutine(FadeOutAndLoad());
    }

    private IEnumerator FadeOutAndLoad()
    {
        yield return FadeTo(1f);
        yield return null;

        SceneManager.LoadScene(targetSceneName);
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float elapsed = 0f;

        if (fadeDuration <= 0f)
        {
            fadeCanvasGroup.alpha = targetAlpha;
            yield break;
        }

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;

            fadeCanvasGroup.alpha = Mathf.Lerp(
                startAlpha,
                targetAlpha,
                elapsed / fadeDuration
            );

            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}
