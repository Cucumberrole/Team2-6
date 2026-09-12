using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [Header("柱の状態")]
    public GameObject intactObject;
    public GameObject brokenTopObject;
    public GameObject brokenBottomObject;

    [Header("当たり判定")]
    public Collider2D objectCollider;

    [Header("破壊エフェクト")]
    public GameObject breakEffect;

    private bool isBroken;

    void Start()
    {
        intactObject.SetActive(true);
        brokenTopObject.SetActive(false);
        brokenBottomObject.SetActive(false);
    }

    public void Break()
    {
        if (isBroken)
        {
            return;
        }

        isBroken = true;

        intactObject.SetActive(false);
        brokenTopObject.SetActive(true);
        brokenBottomObject.SetActive(true);

        if (objectCollider != null)
        {
            objectCollider.enabled = false;
        }

        if (breakEffect != null)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
        }
    }
}