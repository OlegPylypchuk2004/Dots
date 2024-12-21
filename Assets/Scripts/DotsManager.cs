using System.Collections.Generic;
using UnityEngine;

public class DotsManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _dotLayerMask;
    [SerializeField] private LineRenderer _lineRenderer;

    private List<Dot> _selectedDots;

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
            
            if (hitInfo.collider != null && hitInfo.collider.TryGetComponent(out Dot dot) && _selectedDots.Contains(dot) == false)
            {
                _selectedDots.Add(dot);
                _lineRenderer.positionCount = _selectedDots.Count;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _selectedDots[_selectedDots.Count - 1].transform.position);
            }
        }
    }

    private Vector2 MouseWorldPosition()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}