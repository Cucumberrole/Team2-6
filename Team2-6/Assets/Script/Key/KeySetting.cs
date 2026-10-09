using UnityEngine;

public class KeySetting : MonoBehaviour
{
    [Header("このステージで必要な鍵の数")]
    public int stageKeys = 1;

    void Start()
    {
        if (KeyManager.Instance != null)
        {
            KeyManager.Instance.totalKeys = stageKeys;
        }
    }
}
