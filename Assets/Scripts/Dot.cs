using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

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
}