using DG.Tweening;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _animationSpriteRenderer;

    private DotData _data;

    public Point Point { get; set; }

    public DotData Data
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;

            _spriteRenderer.color = value.Color;

            Color targetColor = value.Color;
            targetColor.a = .5f;

            _animationSpriteRenderer.color = targetColor;
        }
    }

    public Tween Appear()
    {
        _collider.enabled = false;
        _animationSpriteRenderer.gameObject.SetActive(false);

        return _spriteRenderer.transform.DOScale(1f, 0.25f)
            .From(0f)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject)
            .OnKill(() =>
            {
                _collider.enabled = true;
                _animationSpriteRenderer.gameObject.SetActive(true);
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
        return _animationSpriteRenderer.transform.DOScale(1.5f, 0.125f)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject);
    }

    public Tween Deselect()
    {
        return _animationSpriteRenderer.transform.DOScale(1f, 0.125f)
            .SetEase(Ease.InQuad)
            .SetLink(gameObject);
    }
}