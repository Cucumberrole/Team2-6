using UnityEngine;

public class UnlockItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            KeyManager.Instance.UnlockLockedKeys();
            Destroy(gameObject);
        }
    }
}