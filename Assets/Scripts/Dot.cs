using DG.Tweening;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;

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
}