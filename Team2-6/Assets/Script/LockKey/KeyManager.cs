using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;

    [Header("ステージの鍵")]
    public int totalKeys = 4;

    private int collectedKeys;
    private bool lockedKeysUnlocked;

    public bool LockedKeysUnlocked => lockedKeysUnlocked;
    public bool HasAllKeys => collectedKeys >= totalKeys;

    void Awake()
    {
        Instance = this;
    }

    // 紫の鍵を取得
    public void CollectKey()
    {
        collectedKeys++;
        Debug.Log("鍵：" + collectedKeys + " / " + totalKeys);

        if (HasAllKeys)
        {
            Debug.Log("すべての鍵を取得しました！");
        }
    }

    // 青いアイテムを取得
    public void UnlockLockedKeys()
    {
        lockedKeysUnlocked = true;
        Debug.Log("ロックされた鍵が解除されました！");
    }
}