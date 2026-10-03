using UnityEngine;

public class KeySetting : MonoBehaviour
{
    //チュートリアル用の鍵
    void Start()
    {
        if (KeyManager.Instance != null)
        {
            KeyManager.Instance.totalKeys = 1;
        }
    }
}
