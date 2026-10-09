using UnityEngine;

public class LockedObject : MonoBehaviour
{
    private bool isUnlocked;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isUnlocked)
        {
            return;
        }

        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        // 解除鍵を持っているか確認
        if (!UnlockKeyManager.Instance.UseKey())
        {
            return;
        }

        isUnlocked = true;

        Debug.Log("障害物を解除しました");

        GameAudioManager.Instance?.PlayUnlock();

        Destroy(gameObject);
    }
}