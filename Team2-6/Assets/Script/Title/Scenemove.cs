using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    [Header("フェード用の黒いパネル")]
    public CanvasGroup fadeCanvasGroup;

    [Header("切り替え先のシーン")]
    public string sceneName = "StageSelect";

    [Header("フェードの片道の時間（秒）")]
    [Min(0f)] public float fadeDuration = 0.5f;

    private bool isTransitioning;

    private void Awake()
    {
        if (fadeCanvasGroup == null)
        {
            Debug.LogError("SceneMove: Fade Canvas Group に黒いパネルを設定してください。", this);
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

        // シーン開始時は黒い画面から徐々に表示する。
        yield return FadeTo(0f);
        fadeCanvasGroup.blocksRaycasts = false;
        isTransitioning = false;
    }

    // ボタンの On Click() から呼び出す。
    public void ChangeScene()
    {
        if (isTransitioning || fadeCanvasGroup == null)
            return;

        if (string.IsNullOrWhiteSpace(sceneName) ||
            !Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError("SceneMove: 切り替え先のシーン名と Build Profiles の Scene List を確認してください。", this);
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
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float elapsed = 0f;
        float duration = fadeDuration;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}
