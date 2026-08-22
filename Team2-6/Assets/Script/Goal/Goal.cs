using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject goalLockObject;

    private bool isOpen;
    private bool isGoal;

    public BackgroundController backgroundController;

    void Update()
    {
        // 鍵をすべて取得したらゴールを開く
        if (!isOpen && KeyManager.Instance.HasAllKeys)
        {
            OpenGoal();
        }
    }

    private void OpenGoal()
    {
        isOpen = true;

        if (goalLockObject != null)
        {
            goalLockObject.SetActive(false);
        }

        Debug.Log("ゴールが開きました！");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpen || isGoal)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            isGoal = true;
            Debug.Log("ゴール！");

            backgroundController.RestoreColor();
        }
    }
}