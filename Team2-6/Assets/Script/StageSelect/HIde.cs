using UnityEngine;
using UnityEngine.EventSystems;

public class HIde : MonoBehaviour
{
    public GameObject hide;
    public GameObject hide1;

    public CanvasGroup canvasGroup;

    public void OnObject()
    {
        GameAudioManager.Instance?.PlayCancel();

        canvasGroup.interactable = true;

        if (Show.lastButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(Show.lastButton);
        }

        hide.SetActive(false);
        hide1.SetActive(false);
    }
}
