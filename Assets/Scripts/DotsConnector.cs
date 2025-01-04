using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DotsConnector : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Field _field;
    [SerializeField] private LayerMask _dotLayerMask;
    [SerializeField] private LineRenderer _lineRenderer;

    private List<Dot> _selectedDots;
    private List<Dot> _extraSelectedDots;
    private bool _isActivated;

    public event Action<Dot[]> DotsConnected;

    private void Awake()
    {
        _selectedDots = new List<Dot>();
        _extraSelectedDots = new List<Dot>();
    }

    private void Update()
    {
        if (!_isActivated || IsTouchOverUI())
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mouseWorldPosition = MouseWorldPosition();
            RaycastHit2D hitInfo = Physics2D.Raycast(mouseWorldPosition, Vector2.zero, 1f, _dotLayerMask);

            if (hitInfo.collider != null && hitInfo.collider.TryGetComponent(out Dot dot))
            {
                if (_selectedDots.Count == 0)
                {
                    Select(dot);
                }
                else if (IsCanConnect(dot, _selectedDots[_selectedDots.Count - 1]))
                {
                    if (_selectedDots.Contains(dot))
                    {
                        if (_selectedDots[_selectedDots.Count - 2] == dot)
                        {
                            _selectedDots[_selectedDots.Count - 1].Deselect();
                            _selectedDots.RemoveAt(_selectedDots.Count - 1);

                            UpdateLine();

                            foreach (Dot extraSelectedDot in _extraSelectedDots)
                            {
                                extraSelectedDot.Deselect();
                            }

                            _extraSelectedDots.Clear();
                        }
                        else
                        {
                            if (_selectedDots.Contains(dot))
                            {
                                Dot[] allDots = _field.AllDots;

                                for (int i = 0; i < allDots.Length; i++)
                                {
                                    if (allDots[i].Data == _selectedDots[0].Data && !_selectedDots.Contains(allDots[i]))
                                    {
                                        _extraSelectedDots.Add(allDots[i]);
                                        allDots[i].Select();
                                    }
                                }
                            }

                            Select(dot);
                        }
                    }
                    else
                    {
                        Select(dot);
                    }
                }
            }
        }
        else
        {
            if (_selectedDots.Count >= 2)
            {
                foreach (Dot extraSelectedDot in _extraSelectedDots)
                {
                    extraSelectedDot.Deselect();
                    _selectedDots.Add(extraSelectedDot);
                }

                DotsConnected?.Invoke(_selectedDots.ToArray());
            }

            foreach (Dot selectedDot in _selectedDots)
            {
                selectedDot.Deselect();
            }

            _selectedDots.Clear();
            _extraSelectedDots.Clear();
            _lineRenderer.positionCount = 0;
        }
    }

    public void Activate()
    {
        _isActivated = true;
    }

    public void Deactivate()
    {
        _isActivated = false;
    }

    private bool IsTouchOverUI()
    {
        EventSystem eventSystem = EventSystem.current;

        if (eventSystem == null)
        {
            return false;
        }

        if (Input.GetMouseButton(0) && eventSystem.IsPointerOverGameObject())
        {
            return true;
        }

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (eventSystem.IsPointerOverGameObject(touch.fingerId))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void Select(Dot dot)
    {
        _selectedDots.Add(dot);
        dot.Select();

        UpdateLine();
    }

    private void UpdateLine()
    {
        if (_selectedDots.Count > 0)
        {
            _lineRenderer.startColor = _selectedDots[0].Data.Color;
            _lineRenderer.endColor = _selectedDots[0].Data.Color;
        }

        _lineRenderer.positionCount = _selectedDots.Count;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _selectedDots[_selectedDots.Count - 1].transform.position);
    }

    private Vector2 MouseWorldPosition()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private bool IsCanConnect(Dot firstDot, Dot secondDot)
    {
        return IsPointsNeighboring(firstDot.Point, secondDot.Point) && firstDot.Data == secondDot.Data;
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