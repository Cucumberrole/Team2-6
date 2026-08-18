using UnityEngine;

public class Key : MonoBehaviour
{
    public static bool hasKey = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasKey = true;
            Destroy(gameObject);
        }
    }
}
