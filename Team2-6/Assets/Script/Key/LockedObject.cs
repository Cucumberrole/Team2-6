using UnityEngine;

public class LockedObject : MonoBehaviour
{
    private bool isUnlocked;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isUnlocked)
        {
            return;
        }

        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        // ‰ğœŒ®‚ğ‚Á‚Ä‚¢‚é‚©Šm”F
        if (!UnlockKeyManager.Instance.UseKey())
        {
            return;
        }

        isUnlocked = true;

        Debug.Log("áŠQ•¨‚ğ‰ğœ‚µ‚Ü‚µ‚½");

        Destroy(gameObject);
    }
}