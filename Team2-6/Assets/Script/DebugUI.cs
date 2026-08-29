using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    public TMP_Text debugText;

    public PlayerHealth playerHealth;
    public PlayerAbility playerAbility;
    public PlayerMove playerMove;

    void Update()
    {
        if (debugText == null ||
            playerHealth == null ||
            playerAbility == null ||
            playerMove == null)
        {
            return;
        }

        string direction = playerMove.FacingDirection > 0 ? "Right" : "Left";

        debugText.text =
            "[ DEBUG ]\n\n" +
            "HP : " + playerHealth.CurrentHp + " / " + playerHealth.maxHp + "\n" +
            "Ability : " + playerAbility.currentAbility + "\n" +
            "Invincible : " + playerHealth.IsInvincible + "\n" +
            "Barrier : " + GetBarrierText() + "\n\n" +
            "Ground : " + playerMove.IsGround + "\n" +
            "Direction : " + direction;
    }

    private string GetBarrierText()
    {
        if (!playerHealth.HasBarrier)
        {
            return "False";
        }

        if (playerHealth.BarrierRemainingTime < 0f)
        {
            return "True  --";
        }

        return "True  " + playerHealth.BarrierRemainingTime.ToString("F1") + "s";
    }
}