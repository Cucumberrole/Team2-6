using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeartUI : MonoBehaviour
{
    [Header("プレイヤーHP")]
    public PlayerHealth playerHealth;

    [Header("ハート設定")]
    public Image heartPrefab;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

    private readonly List<Image> hearts = new();
    private int lastMaxHp = -1;

    void Start()
    {
        CreateHearts();
        UpdateHearts();
    }

    void Update()
    {
        if (playerHealth == null)
        {
            return;
        }

        if (lastMaxHp != playerHealth.maxHp)
        {
            CreateHearts();
        }

        UpdateHearts();
    }

    private void CreateHearts()
    {
        if (playerHealth == null || heartPrefab == null)
        {
            return;
        }

        foreach (Image heart in hearts)
        {
            if (heart != null)
            {
                Destroy(heart.gameObject);
            }
        }

        hearts.Clear();

        for (int i = 0; i < playerHealth.maxHp; i++)
        {
            Image newHeart = Instantiate(heartPrefab, transform);
            newHeart.gameObject.SetActive(true);
            hearts.Add(newHeart);
        }

        lastMaxHp = playerHealth.maxHp;
    }

    private void UpdateHearts()
    {
        if (playerHealth == null)
        {
            return;
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < playerHealth.CurrentHp)
            {
                hearts[i].sprite = fullHeartSprite;
            }
            else
            {
                hearts[i].sprite = emptyHeartSprite;
            }
        }
    }
}
