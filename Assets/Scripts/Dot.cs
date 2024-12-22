using DG.Tweening;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _animationSpriteRenderer;

    public Point Point { get; set; }

    public Color Color
    {
        get
        {
            return _spriteRenderer.color;
        }
        set
        {
            _spriteRenderer.color = value;

            Color targetColor = value;
            targetColor.a = .5f;

            _animationSpriteRenderer.color = targetColor;
        }
    }

    public Tween Appear()
    {
        _collider.enabled = false;

        return _spriteRenderer.transform.DOScale(1f, 0.25f)
            .From(0f)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject)
            .OnKill(() =>
            {
                _collider.enabled = true;
            });
    }

    public Tween MoveTo(Vector2 position)
    {
        _collider.enabled = false;

        return transform.DOMove(position, 0.5f)
            .SetEase(Ease.OutBounce)
            .SetLink(gameObject)
            .OnKill(() =>
            {
                _collider.enabled = true;
            });
    }

    public Tween Select()
    {
        return _animationSpriteRenderer.transform.DOScale(1.5f, 0.25f)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject);
    }

    public Tween Deselect()
    {
        return _animationSpriteRenderer.transform.DOScale(0f, 0.25f)
            .SetEase(Ease.InQuad)
            .SetLink(gameObject);
    }
}