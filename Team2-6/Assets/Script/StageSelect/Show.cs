using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Show : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject show;
    public GameObject show1;

    public CanvasGroup canvasGroup;

    [Header("選択時の大きさ")]
    public float size = 1.1f;

    private Vector3 normalScale;
    private bool canPlaySelectSE;

    public static GameObject lastButton;

    void Start()
    {
        normalScale = transform.localScale;
        StartCoroutine(EnableSelectSE());
    }

    private IEnumerator EnableSelectSE()
    {
        yield return null;
        canPlaySelectSE = true;
    }

    public void OnSelect(BaseEventData eventData)
    {
        transform.localScale = normalScale * size;

        if (canPlaySelectSE)
        {
            GameAudioManager.Instance?.PlaySelect();
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.localScale = normalScale;
    }

    public void OnObject()
    {
        GameAudioManager.Instance?.PlayDecide();

        lastButton = EventSystem.current.currentSelectedGameObject;

        show.SetActive(true);
        show1.SetActive(true);

        canvasGroup.interactable = false;
    }
}
