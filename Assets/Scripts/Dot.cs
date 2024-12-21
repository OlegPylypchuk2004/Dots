using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void Initialize(Color color)
    {
        _spriteRenderer.color = color;
    }

    public Color Color
    {
        get => _spriteRenderer.color;
    }
}