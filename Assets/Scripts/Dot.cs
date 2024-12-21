using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void Initialize(Point point, Color color)
    {
        Point = point;
        _spriteRenderer.color = color;
    }

    public Point Point { get; private set; }

    public Color Color
    {
        get => _spriteRenderer.color;
    }
}