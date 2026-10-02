using System.Collections;
using UnityEngine;

public class BurnableObject : MonoBehaviour
{
    [Header("燃焼時間")]
    [Min(0f)]
    public float burnTime = 2f;

    [Header("表示")]
    public GameObject burningVisual;
    public SpriteRenderer treeSpriteRenderer;
    public Sprite burnedSprite;

    private bool isBurning;
    private bool isBurned;

    private void Awake()
    {
        // 同じオブジェクトの SpriteRenderer は自動で取得する。
        if (treeSpriteRenderer == null)
        {
            treeSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        // 炎は木の子オブジェクトを指定する。
        if (burningVisual != null)
        {
            burningVisual.SetActive(false);
        }
    }

    public void Burn()
    {
        // 燃焼中・燃焼済みの木には、重複して着火しない。
        if (isBurning || isBurned)
        {
            return;
        }

        StartCoroutine(BurnRoutine());
    }

    private IEnumerator BurnRoutine()
    {
        isBurning = true;

        if (burningVisual != null)
        {
            burningVisual.SetActive(true);
        }

        // 燃えている間は、木の当たり判定を維持する。
        yield return new WaitForSeconds(Mathf.Max(0f, burnTime));

        if (burningVisual != null)
        {
            burningVisual.SetActive(false);
        }

        if (treeSpriteRenderer != null && burnedSprite != null)
        {
            treeSpriteRenderer.sprite = burnedSprite;
        }

        // 木本体と子の当たり判定を無効にする。見た目は残す。
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>(true);
        foreach (Collider2D treeCollider in colliders)
        {
            treeCollider.enabled = false;
        }

        isBurned = true;
        isBurning = false;
    }
}
