using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;

    [Header("ステージの鍵")]
    public int totalKeys = 4;

    private int collectedKeys;

    public bool HasAllKeys => collectedKeys >= totalKeys;

    void Awake()
    {
        Instance = this;
    }

    public void CollectKey()
    {
        collectedKeys++;
        Debug.Log("鍵：" + collectedKeys + " / " + totalKeys);

        if (HasAllKeys)
        {
            Debug.Log("すべての鍵を取得しました！");
        }
    }
}