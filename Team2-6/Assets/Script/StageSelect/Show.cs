using UnityEngine;
using UnityEngine.EventSystems;

public class Show : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject show;
    public GameObject show1;

    public CanvasGroup canvasGroup;

    // 選択時の大きさ
    public float size = 1.1f;

    private Vector3 normalScale;

    // 開くときに押したボタンを記憶
    public static GameObject lastButton;

    void Start()
    {
        // 最初の大きさを保存
        normalScale = transform.localScale;
    }

    // WASDで選択されたとき
    public void OnSelect(BaseEventData eventData)
    {
        transform.localScale = normalScale * size;
    }

    // 選択が外れたとき
    public void OnDeselect(BaseEventData eventData)
    {
        transform.localScale = normalScale;
    }

    public void OnObject()
    {
        // 現在選択しているボタンを保存
        lastButton = EventSystem.current.currentSelectedGameObject;

        show.SetActive(true);
        show1.SetActive(true);

        // 元のCanvasを操作不能
        canvasGroup.interactable = false;
    }
}