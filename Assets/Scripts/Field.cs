using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Dot _dotPrefab;
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _spacing;

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        Vector2 startPosition = new Vector2(-((_width - 1) * _spacing) / 2, -((_height - 1) * _spacing) / 2);

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                Vector2 spawnPosition = startPosition + new Vector2(x * _spacing, y * _spacing);

                Instantiate(_dotPrefab, spawnPosition, Quaternion.identity, transform);
            }
        }
    }
}