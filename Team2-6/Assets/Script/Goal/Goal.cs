using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject goalLockObject;
    public BackgroundController backgroundController;
    public string stageSelectSceneName = "StageSelect";

    private bool isOpen;
    private bool isGoal;

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
            StartCoroutine(GoalSequence(other));
        }
    }

    private IEnumerator GoalSequence(Collider2D player)
    {
        // Playerの操作を停止
        PlayerMove playerMove = player.GetComponentInParent<PlayerMove>();
        Rigidbody2D rb = player.GetComponentInParent<Rigidbody2D>();

        if (playerMove != null)
        {
            playerMove.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // 背景が完全にカラーになるまで待つ
        if (backgroundController != null)
        {
            yield return StartCoroutine(backgroundController.RestoreColorRoutine());
        }

        // ステージセレクトへ戻る
        SceneManager.LoadScene(stageSelectSceneName);
    }
}