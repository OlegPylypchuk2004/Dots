using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Point _pointPrefab;
    [SerializeField] private Dot _dotPrefab;
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _spacing;

    private DotData _dotData;
    private List<Point> _points;

    private void Awake()
    {
        _points = new List<Point>();
    }

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
                Point point = SpawnPoint(startPosition + new Vector2(x * _spacing, y * _spacing));
                _points.Add(point);
            }
        }

        for (int i = 0; i < _points.Count; i++)
        {
            Dot dot = SpawnDot();
            dot.transform.SetParent(_points[i].transform, false);
            dot.Initialize(_dotData.Colors[Random.Range(0, _dotData.Colors.Length)]);
        }
    }

    private Point SpawnPoint(Vector2 position)
    {
        return Instantiate(_pointPrefab, position, Quaternion.identity, transform);
    }

    private Dot SpawnDot()
    {
        return Instantiate(_dotPrefab);
    }
}