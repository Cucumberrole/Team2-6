using UnityEngine;

public class LockedObject : MonoBehaviour
{
    public Collider2D keyCollider;
    public GameObject lockObject;

    void Start()
    {
        // Å‰‚Í‡‚ÌŒ®‚ğæ“¾‚Å‚«‚È‚¢
        if (keyCollider != null)
        {
            keyCollider.enabled = false;
        }
    }

    public void Unlock()
    {
        // ‡‚ÌŒ®‚ğæ“¾‰Â”\‚É‚·‚é
        if (keyCollider != null)
        {
            keyCollider.enabled = true;
        }

        // ƒƒbƒN‚µ‚Ä‚¢‚é” ‚ğÁ‚·
        if (lockObject != null)
        {
            Destroy(lockObject);
        }
    }
}