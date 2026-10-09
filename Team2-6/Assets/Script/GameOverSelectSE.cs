using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverSelectSE : MonoBehaviour, ISelectHandler
{
    private bool canPlaySelectSE;

    void OnEnable()
    {
        StartCoroutine(EnableSelectSE());
    }

    private IEnumerator EnableSelectSE()
    {
        yield return null;
        canPlaySelectSE = true;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (!canPlaySelectSE)
        {
            return;
        }

        GameAudioManager.Instance?.PlaySelect();
    }
}
