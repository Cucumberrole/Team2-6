using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    [Header("フェード用の黒いパネル")]
    public CanvasGroup fadeCanvasGroup;

    [Header("切り替え先のシーン")]
    public string FastSceneName = "Tutorial";
    public string SecondSceneName = "StageSelect";

    [Header("フェードの片道の時間（秒）")]
    [Min(0f)] public float fadeDuration = 0.5f;

    private bool isTransitioning;
    private string targetSceneName;

    private static bool tutorialPlayed = false;

    private void Awake()
    {
        if (fadeCanvasGroup == null)
        {
            Debug.LogError("Fade Canvas Groupを設定してください。");
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
            yield break;

        yield return FadeTo(0f);

        fadeCanvasGroup.blocksRaycasts = false;
        isTransitioning = false;
    }

    // ボタンの On Click() から呼び出す。
    public void ChangeScene()
    {
        if (isTransitioning || fadeCanvasGroup == null)
            return;

        // ゲーム起動後、最初の1回
        if (!tutorialPlayed)
        {
            targetSceneName = FastSceneName;

            // Tutorialへ行ったことを記録
            tutorialPlayed = true;
        }
        else
        {
            // 2回目以降
            targetSceneName = SecondSceneName;
        }

        if (!Application.CanStreamedLevelBeLoaded(targetSceneName))
        {
            Debug.LogError(
                "シーン「" + targetSceneName + "」がScene Listにありません。"
            );
            return;
        }

        isTransitioning = true;
        fadeCanvasGroup.blocksRaycasts = true;

        StartCoroutine(FadeOutAndLoad());
    }

    private IEnumerator FadeOutAndLoad()
    {
        yield return FadeTo(1f);
        // 黒い画面を一度描画してから読み込む。
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
