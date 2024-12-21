using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _rayOffset;
    [SerializeField] private float _rayDistance;

    public Point UpPoint { get; private set; }
    public Point DownPoint { get; private set; }
    public Point RightPoint { get; private set; }
    public Point LeftPoint { get; private set; }
    public Dot Dot { get; set; }

    public void FindNeighboring()
    {
        UpPoint = null;
        DownPoint = null;
        RightPoint = null;
        LeftPoint = null;

        Ray2D rayUp = new Ray2D(new Vector2(transform.position.x, transform.position.y + _rayOffset), Vector2.up);
        Ray2D rayDown = new Ray2D(new Vector2(transform.position.x, transform.position.y - _rayOffset), Vector2.down);
        Ray2D rayRight = new Ray2D(new Vector2(transform.position.x + _rayOffset, transform.position.y), Vector2.right);
        Ray2D rayLeft = new Ray2D(new Vector2(transform.position.x - _rayOffset, transform.position.y), Vector2.left);

        RaycastHit2D hitUp = Physics2D.Raycast(rayUp.origin, rayUp.direction, _rayDistance, _layerMask);
        RaycastHit2D hitDown = Physics2D.Raycast(rayDown.origin, rayDown.direction, _rayDistance, _layerMask);
        RaycastHit2D hitRight = Physics2D.Raycast(rayRight.origin, rayRight.direction, _rayDistance, _layerMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(rayLeft.origin, rayLeft.direction, _rayDistance, _layerMask);

        if (hitUp.collider != null && hitUp.collider.TryGetComponent(out Point upPoint))
        {
            UpPoint = upPoint;
        }

        if (hitDown.collider != null && hitDown.collider.TryGetComponent(out Point downPoint))
        {
            DownPoint = downPoint;
        }

        if (hitRight.collider != null && hitRight.collider.TryGetComponent(out Point rightPoint))
        {
            RightPoint = rightPoint;
        }

        if (hitLeft.collider != null && hitLeft.collider.TryGetComponent(out Point leftPoint))
        {
            LeftPoint = leftPoint;
        }
    }
}