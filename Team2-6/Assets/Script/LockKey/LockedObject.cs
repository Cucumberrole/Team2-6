using UnityEngine;

public class LockedObject : MonoBehaviour
{
    public Collider2D keyCollider;
    public GameObject lockVisual;

    private bool isUnlocked;

    void Start()
    {
        if (keyCollider == null)
        {
            keyCollider = GetComponent<Collider2D>();
        }

        // Å‰‚ÍŒ®‚ğæ“¾‚Å‚«‚È‚¢
        if (keyCollider != null)
        {
            keyCollider.enabled = false;
        }
    }

    void Update()
    {
        if (!isUnlocked && KeyManager.Instance.LockedKeysUnlocked)
        {
            Unlock();
        }
    }

    private void Unlock()
    {
        isUnlocked = true;

        if (keyCollider != null)
        {
            keyCollider.enabled = true;
        }

        if (lockVisual != null)
        {
            lockVisual.SetActive(false);
        }
    }
}