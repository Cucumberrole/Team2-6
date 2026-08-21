//using UnityEngine;

//public class Key : MonoBehaviour
//{
//    public static bool hasKey = false;

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            hasKey = true;
//            Destroy(gameObject);
//        }
//    }
//}

using UnityEngine;

public class Key : MonoBehaviour
{
    private bool collected;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            collected = true;
            KeyManager.Instance.CollectKey();
            Destroy(gameObject);
        }
    }
}