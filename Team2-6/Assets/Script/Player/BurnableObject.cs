using System.Collections;
using UnityEngine;

public class BurnableObject : MonoBehaviour
{
    public float burnTime = 1f;
    public GameObject burningVisual;

    private bool isBurning;

    public void Burn()
    {
        if (!isBurning)
        {
            StartCoroutine(BurnRoutine());
        }
    }

    private IEnumerator BurnRoutine()
    {
        isBurning = true;

        if (burningVisual != null)
        {
            burningVisual.SetActive(true);
        }

        yield return new WaitForSeconds(burnTime);
        Destroy(gameObject);
    }
}
