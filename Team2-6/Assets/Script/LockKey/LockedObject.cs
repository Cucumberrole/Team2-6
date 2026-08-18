using UnityEngine;

public class LockedObject : MonoBehaviour
{
    private Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        // 鍵を取ったらオブジェクトを消す
        if (Key.hasKey)
        {
            Destroy(gameObject);
        }
    }
}
