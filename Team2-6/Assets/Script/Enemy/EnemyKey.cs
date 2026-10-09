
using UnityEngine;

public class EnemyKey : MonoBehaviour
{
    [Header("“G‚ÌHPŠÇ—")]
    public EnemyMove enemyMove;

    [Header("—‚Æ‚·Œ®‚ÌPrefab")]
    public GameObject keyPrefab;

    [Header("Œ®‚ğ—‚Æ‚·ˆÊ’u")]
    public Transform dropPoint;

    private bool isDropped = false;

    void Start()
    {
        if (enemyMove == null)
        {
            enemyMove = GetComponent<EnemyMove>();
        }
    }

    void Update()
    {
        if (enemyMove == null || isDropped)
            return;

        // HP‚ª0‚É‚È‚Á‚½‚çŒ®‚ğ—‚Æ‚·
        if (enemyMove.EnemyHP <= 0)
        {
            DropKey();
        }
    }

    public void DropKey()
    {
        if (isDropped || keyPrefab == null)
            return;

        isDropped = true;

        Vector3 position = dropPoint != null
            ? dropPoint.position
            : transform.position;

        // Œ®‚ğ‚»‚Ìê‚É¶¬
        Instantiate(keyPrefab, position, Quaternion.identity);
    }
}
