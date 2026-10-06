using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeepKeyboardSelection : MonoBehaviour
{
    public Button firstButton;
    public Button secondButton;

    private GameObject lastSelected;

    void OnEnable()
    {
        StartCoroutine(SelectFirstButton());
    }

    IEnumerator SelectFirstButton()
    {
        yield return null;

        EventSystem.current.SetSelectedGameObject(null);
        if (firstButton != null && firstButton.gameObject.activeInHierarchy)
        {
            firstButton.Select();
            lastSelected = firstButton.gameObject;
        }
        else if (secondButton != null && secondButton.gameObject.activeInHierarchy)
        {
            secondButton.Select();
            lastSelected = secondButton.gameObject;
        }
    }

    void Update()
    {
        if (EventSystem.current == null)
            return;

        GameObject current = EventSystem.current.currentSelectedGameObject;

        if (current != null)
        {
            lastSelected = current;
        }

        // ëIëÇ™è¡Ç¶ÇΩèÍçá
        if (current == null)
        {
            // ç≈å„Ç…ëIëÇµÇƒÇ¢ÇΩButtonÇ™Ç‹ÇæóLå¯
            if (lastSelected != null && lastSelected.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(lastSelected);
            }
            // firstButton
            else if (firstButton != null && firstButton.gameObject.activeInHierarchy)
            {
                firstButton.Select();
                lastSelected = firstButton.gameObject;
            }
            // firstButtonÇ™ñ≥ÇØÇÍÇŒsecondButton
            else if (secondButton != null && secondButton.gameObject.activeInHierarchy)
            {
                secondButton.Select();
                lastSelected = secondButton.gameObject;
            }
        }
    }
}