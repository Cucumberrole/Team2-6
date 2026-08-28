using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeepKeyboardSelection : MonoBehaviour
{
    public Button firstButton;

    private GameObject lastSelected;

    void OnEnable()
    {
        StartCoroutine(SelectFirstButton());
    }

    IEnumerator SelectFirstButton()
    {
        yield return null;

        // EventSystemがない場合
        if (EventSystem.current == null)
        {
            Debug.LogWarning("EventSystemがありません");
            yield break;
        }

        // FirstButtonが設定されていない場合
        if (firstButton == null)
        {
            Debug.LogWarning("First Buttonが設定されていません");
            yield break;
        }

        EventSystem.current.SetSelectedGameObject(null);
        firstButton.Select();

        lastSelected = firstButton.gameObject;
    }

    void Update()
    {
        // EventSystemがなければ何もしない
        if (EventSystem.current == null)
            return;

        GameObject current = EventSystem.current.currentSelectedGameObject;

        // 現在選択されているものを保存
        if (current != null)
        {
            lastSelected = current;
        }

        // 選択が消えた場合
        if (current == null)
        {
            // 前回のボタンがまだ存在・有効なら戻す
            if (lastSelected != null && lastSelected.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(lastSelected);
            }
            // なければFirstButtonに戻す
            else if (firstButton != null && firstButton.gameObject.activeInHierarchy)
            {
                firstButton.Select();
                lastSelected = firstButton.gameObject;
            }
        }
    }
}