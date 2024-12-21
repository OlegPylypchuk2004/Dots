using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Dot _dotPrefab;
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _spacing;

    private DotData _dotData;

    private void Start()
    {
        _dotData = Resources.Load<DotData>("Data/DotData");

        Generate();
    }

    private void Generate()
    {
        Vector2 startPosition = new Vector2(-((_width - 1) * _spacing) / 2, -((_height - 1) * _spacing) / 2);

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                Dot dot = SpawnDot(startPosition + new Vector2(x * _spacing, y * _spacing));
                dot.Initialize(_dotData.Colors[Random.Range(0, _dotData.Colors.Length)]);
            }
        }
    }

    private Dot SpawnDot(Vector2 position)
    {
        return Instantiate(_dotPrefab, position, Quaternion.identity, transform);
    }
}