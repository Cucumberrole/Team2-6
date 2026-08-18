using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private bool isGoal = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // すでにゴール済みなら処理しない
        if (isGoal) return;

        // プレイヤーに触れたらゴール
        if (collision.CompareTag("Player"))
        {
            isGoal = true;
            Debug.Log("ゴール！");

            // ここにゴール後の処理を書く
            // 例：シーン切り替え、UI表示、プレイヤー停止など
        }
    }
}
