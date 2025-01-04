using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Point _pointPrefab;
    [SerializeField] private Dot _dotPrefab;
    [SerializeField] private float _spacing;
    [SerializeField] private DotsConnector _connector;

    private DotData[] _dotDatas;
    private List<Point> _points;
    private List<Dot> _allDots;

    private void Awake()
    {
        _points = new List<Point>();
        _dotDatas = Resources.LoadAll<DotData>("Data/Dots");
        _allDots = new List<Dot>();
    }

    private void OnEnable()
    {
        _connector.DotsConnected += OnDotsConnected;
    }

    private void OnDisable()
    {
        _connector.DotsConnected -= OnDotsConnected;
    }

    public Dot[] AllDots
    {
        get
        {
            return _allDots.ToArray();
        }
    }

    public void Generate(GridData gridData)
    {
        Vector2 startPosition = new Vector2(-((gridData.Width - 1) * _spacing) / 2, -((gridData.Height - 1) * _spacing) / 2);

        for (int y = 0; y < gridData.Height; y++)
        {
            for (int x = 0; x < gridData.Width; x++)
            {
                int index = y * gridData.Width + x;

                if (gridData.Values[index])
                {
                    Vector2 spawnPosition = startPosition + new Vector2(x * _spacing, y * _spacing);
                    Point point = SpawnPoint(spawnPosition);
                    _points.Add(point);
                }
            }
        }

        for (int i = 0; i < _points.Count; i++)
        {
            _points[i].FindNeighboring();

            Dot dot = SpawnDot();
            dot.transform.SetParent(_points[i].transform, false);
            dot.Point = _points[i];
            dot.Data = _dotDatas[Random.Range(0, _dotDatas.Length)];

            _points[i].Dot = dot;
        }
    }

    private Point SpawnPoint(Vector2 position)
    {
        return Instantiate(_pointPrefab, position, Quaternion.identity, transform);
    }

    private Dot SpawnDot()
    {
        Dot dot = Instantiate(_dotPrefab);
        _allDots.Add(dot);

        return dot;
    }

    private void OnDotsConnected(Dot[] dots)
    {
        List<Point> points = new List<Point>();

        foreach (Dot dot in dots)
        {
            points.Add(dot.Point);
            Destroy(dot.gameObject);
            _allDots.Remove(dot);
        }

        foreach (Point point in points)
        {
            point.Dot = null;
        }

        bool isMoved;

        Sequence dotsMoveSequence = DOTween.Sequence();

        do
        {
            isMoved = false;

            foreach (Point point in _points)
            {
                if (point.Dot == null)
                {
                    if (point.UpPoint != null && point.UpPoint.Dot != null)
                    {
                        Dot movingDot = point.UpPoint.Dot;
                        point.Dot = movingDot;
                        movingDot.Point = point;

                        movingDot.transform.SetParent(point.transform);

                        dotsMoveSequence.Join
                            (movingDot.MoveTo(point.transform.position));

                        point.UpPoint.Dot = null;
                        isMoved = true;
                    }
                }
            }
        }
        while (isMoved);

        dotsMoveSequence.SetLink(gameObject);

        dotsMoveSequence.OnKill(() =>
        {
            for (int i = 0; i < _points.Count; i++)
            {
                if (_points[i].Dot == null)
                {
                    Dot dot = SpawnDot();
                    dot.transform.SetParent(_points[i].transform, false);
                    dot.Point = _points[i];
                    dot.Data = _dotDatas[Random.Range(0, _dotDatas.Length)];
                    dot.Appear();

                    _points[i].Dot = dot;
                }
            }
        });
    }
}