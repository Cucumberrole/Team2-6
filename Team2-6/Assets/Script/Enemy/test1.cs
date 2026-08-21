using TMPro;
using UnityEngine;

public class test1 : MonoBehaviour
{
    public TMP_Text hpText;
    public PlayerHealth playerHealth;

    void Update()
    {
        hpText.text = "HP " + playerHealth.CurrentHp.ToString();
    }
}