using UnityEngine;
using UnityEngine.EventSystems;

public class HIde : MonoBehaviour
{
    public GameObject hide;
    public GameObject hide1;

    public CanvasGroup canvasGroup;

    public void OnObject()
    {
        // 元のCanvasを操作可能に戻す
        canvasGroup.interactable = true;

        // 前に押したボタンを選択
        if (Show.lastButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(Show.lastButton);
        }

        // 最後に閉じる
        hide.SetActive(false);
        hide1.SetActive(false);
    }
}