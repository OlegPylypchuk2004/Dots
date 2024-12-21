using System;
using System.Collections.Generic;
using UnityEngine;

public class DotsConnector : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _dotLayerMask;
    [SerializeField] private LineRenderer _lineRenderer;

    private List<Dot> _selectedDots;

    public event Action<Dot[]> DotsConnected;

    private void Awake()
    {
        _selectedDots = new List<Dot>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mouseWorldPosition = MouseWorldPosition();
            RaycastHit2D hitInfo = Physics2D.Raycast(mouseWorldPosition, Vector2.zero, 1f, _dotLayerMask);

            if (hitInfo.collider != null && hitInfo.collider.TryGetComponent(out Dot dot))
            {
                if (_selectedDots.Count == 0 || IsCanConnect(dot, _selectedDots[_selectedDots.Count - 1]))
                {
                    _selectedDots.Add(dot);
                    _lineRenderer.positionCount = _selectedDots.Count;
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _selectedDots[_selectedDots.Count - 1].transform.position);

                    _lineRenderer.startColor = dot.Color;
                    _lineRenderer.endColor = dot.Color;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_selectedDots.Count >= 3)
            {
                DotsConnected?.Invoke(_selectedDots.ToArray());
            }

            _selectedDots.Clear();
            _lineRenderer.positionCount = 0;
        }
    }

    private Vector2 MouseWorldPosition()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private bool IsCanConnect(Dot firstDot, Dot secondDot)
    {
        return !_selectedDots.Contains(firstDot) && IsPointsNeighboring(firstDot.Point, secondDot.Point) && firstDot.Color == secondDot.Color;
    }

    private bool IsPointsNeighboring(Point firstPoint, Point secondPoint)
    {
        if (firstPoint.UpPoint == secondPoint)
        {
            return true;
        }
        else if (firstPoint.DownPoint == secondPoint)
        {
            return true;
        }
        else if (firstPoint.RightPoint == secondPoint)
        {
            return true;
        }
        else if (firstPoint.LeftPoint == secondPoint)
        {
            return true;
        }

        return false;
    }
}