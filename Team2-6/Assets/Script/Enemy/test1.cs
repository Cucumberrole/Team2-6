using TMPro;
using UnityEngine;

public class test1 : MonoBehaviour
{
    public TMP_Text hpText;

    void Update()
    {
        hpText.text = EnemyMove.PlayerHP.ToString();
    }

}
