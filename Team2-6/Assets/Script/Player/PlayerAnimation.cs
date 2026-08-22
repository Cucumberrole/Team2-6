using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("右向き")]
    public Sprite rightFront;
    public Sprite rightBack;
    public Sprite rightJump;

    [Header("左向き")]
    public Sprite leftFront;
    public Sprite leftBack;
    public Sprite leftJump;

    [Header("歩行アニメーション")]
    public float animationSpeed = 0.15f;

    private SpriteRenderer spriteRenderer;
    private PlayerMove playerMove;
    private float animationTimer;
    private bool useFrontFrame = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {
        // 空中ではジャンプ画像
        if (!playerMove.IsGround)
        {
            spriteRenderer.sprite = playerMove.FacingDirection > 0 ? rightJump : leftJump;
            return;
        }

        // 停止中
        if (Mathf.Abs(playerMove.HorizontalInput) < 0.01f)
        {
            spriteRenderer.sprite = playerMove.FacingDirection > 0 ? rightFront : leftFront;
            animationTimer = 0f;
            useFrontFrame = true;
            return;
        }

        // 移動中は2枚を交互に表示
        animationTimer += Time.deltaTime;

        if (animationTimer >= animationSpeed)
        {
            animationTimer = 0f;
            useFrontFrame = !useFrontFrame;
        }

        if (playerMove.FacingDirection > 0)
        {
            spriteRenderer.sprite = useFrontFrame ? rightFront : rightBack;
        }
        else
        {
            spriteRenderer.sprite = useFrontFrame ? leftFront : leftBack;
        }
    }
}