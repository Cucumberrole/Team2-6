using UnityEngine;

public class UnlockKeyManager : MonoBehaviour
{
    public static UnlockKeyManager Instance;

    private int unlockKeys;

    void Awake()
    {
        Instance = this;
    }

    public void AddKey()
    {
        unlockKeys++;
        Debug.Log("‰ğœŒ®æ“¾ : " + unlockKeys);
    }

    public bool UseKey()
    {
        if (unlockKeys <= 0)
        {
            Debug.Log("‰ğœŒ®‚ª‘«‚è‚Ü‚¹‚ñ");
            return false;
        }

        unlockKeys--;
        Debug.Log("‰ğœŒ®g—p c‚è : " + unlockKeys);
        return true;
    }

    public int CurrentKeys => unlockKeys;
}