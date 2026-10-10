using System.Collections;
using UnityEngine;

public class EnemyBurn : MonoBehaviour
{
    [Header("燃えてから消えるまでの時間")]
    [Min(0f)]
    public float burnTime = 2f;

    [Header("敵の子に配置した炎")]
    public GameObject burningVisual;

    [Header("燃焼中に停止する移動・攻撃スクリプト")]
    public MonoBehaviour[] scriptsToDisable = new MonoBehaviour[0];

    [Header("敵本体のアニメーター（必要なら指定）")]
    public Animator enemyAnimator;

    public bool IsBurning { get; private set; }

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (enemyAnimator == null)
        {
            enemyAnimator = GetComponent<Animator>();
        }

        if (burningVisual != null)
        {
            burningVisual.SetActive(false);
        }
    }

    public void Burn()
    {
        // 追加で弾が当たっても、燃焼時間をやり直さない。
        if (IsBurning)
        {
            return;
        }

        IsBurning = true;

        // 凍結の終了処理によって、途中で動き出すことを防ぐ。
        EnemyFreeze enemyFreeze = GetComponent<EnemyFreeze>();
        if (enemyFreeze != null)
        {
            enemyFreeze.StopForBurning();
        }

        if (scriptsToDisable != null)
        {
            foreach (MonoBehaviour script in scriptsToDisable)
            {
                // 自分を停止すると燃焼処理に影響するので除外する。
                if (script != null && script != this)
                {
                    script.StopAllCoroutines();
                    script.enabled = false;
                }
            }
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (enemyAnimator != null)
        {
            enemyAnimator.speed = 0f;
        }

        if (burningVisual != null)
        {
            burningVisual.SetActive(true);
        }

        StartCoroutine(BurnRoutine());
    }

    private IEnumerator BurnRoutine()
    {
        yield return new WaitForSeconds(Mathf.Max(0f, burnTime));
        // 子に置いた炎も、敵と一緒に消える。
        EnemyMove enemyMove = GetComponent<EnemyMove>();

        if (enemyMove != null)
        {
            enemyMove.Defeat();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
