using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamecleargoal : MonoBehaviour
{
    public GameObject goalLockObject;
    public BackgroundController backgroundController;
    public string stageSelectSceneName = "StageSelect";

    [Header("BGM")]
    public AudioSource stageBgmSource;

    private bool isOpen;
    public static bool isGoal;

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

        if (stageBgmSource != null)
        {
            stageBgmSource.Stop();
        }

        float jingleLength = 0f;

        if (GameAudioManager.Instance != null)
        {
            jingleLength = GameAudioManager.Instance.PlayClearJingle();
        }

        Coroutine backgroundRoutine = null;

        if (backgroundController != null)
        {
            backgroundRoutine = StartCoroutine(
                backgroundController.RestoreColorRoutine()
            );
        }

        if (jingleLength > 0f)
        {
            yield return new WaitForSeconds(jingleLength);
        }
        else if (backgroundRoutine != null)
        {
            yield return backgroundRoutine;
        }

        SceneManager.LoadScene("GameClear");
    }
}