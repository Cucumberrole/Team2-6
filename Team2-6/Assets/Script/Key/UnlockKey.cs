using UnityEngine;

public class UnlockKey : MonoBehaviour
{
    private bool collected;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected || !other.CompareTag("Player"))
        {
            return;
        }

        collected = true;

        UnlockKeyManager.Instance.AddKey();

        Destroy(gameObject);
    }
}