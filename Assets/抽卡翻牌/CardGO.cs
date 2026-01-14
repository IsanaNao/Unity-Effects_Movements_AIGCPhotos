using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CardGO : MonoBehaviour
{
    [Header("Card Parts")]
    [SerializeField] private SpriteRenderer front;
    [SerializeField] private SpriteRenderer back;

    [Header("Size Control")]
    [SerializeField] private Vector2 targetWorldSize = new Vector2(1f, 1.4f);

    [Header("Flip")]
    [SerializeField] private float flipDuration = 0.25f;

    private bool flipped = false;
    private bool animating = false;

    private void Start()
    {
        // 初始化朝向
        transform.rotation = Quaternion.identity;
        flipped = false;

        // 统一尺寸（只做一次）
        FitSprite(front);
        FitSprite(back);
    }

    private void FitSprite(SpriteRenderer sr)
    {
        if (sr == null || sr.sprite == null) return;

        Vector2 spriteSize = sr.sprite.bounds.size;
        float scale = Mathf.Min(
            targetWorldSize.x / spriteSize.x,
            targetWorldSize.y / spriteSize.y
        );

        sr.transform.localScale = Vector3.one * scale;
    }

    private void OnMouseDown()
    {
        Flip();
    }

    private void Flip()
    {
        if (animating) return;
        animating = true;

        transform.DOKill();

        float targetY = flipped ? 0f : 180f;
        flipped = !flipped;

        transform
            .DORotate(new Vector3(0, targetY, 0), flipDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                animating = false;
            });
    }
}
