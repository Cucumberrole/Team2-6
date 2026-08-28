using UnityEngine;

public class UnlockItem : MonoBehaviour
{
    public LockedObject[] lockedObjects;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (LockedObject lockedObject in lockedObjects)
            {
                if (lockedObject != null)
                {
                    lockedObject.Unlock();
                }
            }

            Destroy(gameObject);
        }
    }
}