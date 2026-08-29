using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonSelect : MonoBehaviour
{
    void Start()
    {
        SetupNavigation();
    }

    public void SetupNavigation()
    {
        Button[] buttons = FindObjectsOfType<Button>(true);

        foreach (Button button in buttons)
        {
            if (!button.gameObject.activeInHierarchy)
                continue;

            Navigation nav = button.navigation;
            nav.mode = Navigation.Mode.Explicit;

            nav.selectOnUp = FindNearest(button, buttons, Vector2.up);
            nav.selectOnDown = FindNearest(button, buttons, Vector2.down);
            nav.selectOnLeft = FindNearest(button, buttons, Vector2.left);
            nav.selectOnRight = FindNearest(button, buttons, Vector2.right);

            button.navigation = nav;
        }
    }

    Button FindNearest(Button current, Button[] buttons, Vector2 direction)
    {
        Button bestButton = null;
        float bestScore = float.MaxValue;

        Vector2 currentPos =
            current.transform.position;

        foreach (Button other in buttons)
        {
            if (other == current)
                continue;

            if (!other.gameObject.activeInHierarchy)
                continue;

            if (!other.interactable)
                continue;

            Vector2 otherPos =
                other.transform.position;

            Vector2 delta =
                otherPos - currentPos;

            // 指定方向にないボタンは除外
            if (Vector2.Dot(delta.normalized, direction) <= 0.5f)
                continue;

            float distance = delta.magnitude;

            // 方向のズレ
            float angle =
                Vector2.Angle(direction, delta.normalized);

            // 距離＋角度で判定
            float score =
                distance + angle * 10f;

            if (score < bestScore)
            {
                bestScore = score;
                bestButton = other;
            }
        }

        return bestButton;
    }
}