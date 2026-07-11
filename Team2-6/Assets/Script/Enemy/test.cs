using TMPro;
using UnityEngine;

public class test : MonoBehaviour
{
    public TMP_Text hpText;

    void Update()
    {
        hpText.text = EnemyMove.EnemyHP.ToString();
    }

}
