using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionState : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    public bool IsRoofed { get; private set; }
    public bool IsOnWallLeft { get; private set; }
    public bool IsOnWallRight { get; private set; }

    [SerializeField]
    private Transform
        _raycastOriginTopLeft,
        _raycastOriginTopRight,
        _raycastOriginBottomLeft,
        _raycastOriginBottomRight;

    [SerializeField]
    private LayerMask
        _groundLayer,
        _sawbladeLayer;

    [SerializeField]
    private int
        _horizontalRaycastCount = 8,
        _verticalRaycastCount = 8;

    [SerializeField]
    private float
        _raycastDistance = .5f;

    private void Update()
    {
        IsGrounded = CheckForGround();
        IsRoofed = CheckForRoof();
        IsOnWallLeft = CheckForWallLeft();
        IsOnWallRight = CheckForWallRight();

        //Debug.Log("grounded: " + IsGrounded);
        //Debug.Log("wall left: " + IsOnWallLeft + " wall right: " + IsOnWallRight + " grounded: " + IsGrounded);
    }

    private bool CheckForGround()
    {
        var startX = _raycastOriginBottomLeft.position.x;
        var endX = _raycastOriginBottomRight.position.x;
        var positionY = _raycastOriginBottomLeft.position.y;
        var incrementX = (endX - startX) / (_horizontalRaycastCount - 1);
        var raycastPositionX = startX;

        for (var i = 0; i < _horizontalRaycastCount; i++)
        {
            var raycastOrigin = new Vector2(raycastPositionX, positionY);
            var groundHit = Physics2D.Raycast(raycastOrigin, Vector2.down, _raycastDistance, _groundLayer);

            if (groundHit)
                return true;

            raycastPositionX += incrementX;
        }

        return false;
    }

    private bool CheckForRoof()
    {
        var startX = _raycastOriginTopLeft.position.x;
        var endX = _raycastOriginTopRight.position.x;
        var positionY = _raycastOriginTopLeft.position.y;
        var incrementX = (endX - startX) / (_horizontalRaycastCount - 1);
        var raycastPositionX = startX;

        for (var i = 0; i < _horizontalRaycastCount; i++)
        {
            var raycastOrigin = new Vector2(raycastPositionX, positionY);
            var roofHit = Physics2D.Raycast(raycastOrigin, Vector2.up, _raycastDistance, _groundLayer);

            if (roofHit)
                return true;

            raycastPositionX += incrementX;
        }

        return false;
    }

    private bool CheckForWallLeft()
    {
        var startY = _raycastOriginTopRight.position.y;
        var endY = _raycastOriginBottomRight.position.y;
        var positionX = _raycastOriginTopLeft.position.x;
        var incrementY = (endY - startY) / (_verticalRaycastCount - 1);
        var raycastPositionY = startY;

        for (var i = 0; i < _verticalRaycastCount; i++)
        {
            var raycastOrigin = new Vector2(positionX, raycastPositionY);
            var wallHit = Physics2D.Raycast(raycastOrigin, Vector2.left, _raycastDistance, _groundLayer);

            if (wallHit)
                return true;

            raycastPositionY += incrementY;
        }

        return false;
    }

    private bool CheckForWallRight()
    {
        var startY = _raycastOriginTopRight.position.y;
        var endY = _raycastOriginBottomRight.position.y;
        var positionX = _raycastOriginTopRight.position.x;
        var incrementY = (endY - startY) / (_verticalRaycastCount - 1);
        var raycastPositionY = startY;

        for (var i = 0; i < _verticalRaycastCount; i++)
        {
            var raycastOrigin = new Vector2(positionX, raycastPositionY);
            var wallHit = Physics2D.Raycast(raycastOrigin, Vector2.right, _raycastDistance, _groundLayer);

            if (wallHit)
                return true;

            raycastPositionY += incrementY;
        }

        return false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Sawblade"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 contactPoint = contact.point;
                Vector2 center = collision.collider.bounds.center;

                if (contactPoint.x > center.x)
                {
                    Debug.Log("Collision on the right side");

                    gameObject.SetActive(false);
                }
                else if (contactPoint.x < center.x)
                {
                    Debug.Log("Collision on the left side");

                    gameObject.SetActive(false);
                }

                if (contactPoint.y > center.y)
                {
                    Debug.Log("Collision on the top side");
                }
                else if (contactPoint.y < center.y)
                {
                    Debug.Log("Collision on the bottom side");

                    gameObject.SetActive(false);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        // gizmo for ground check
        var startX = _raycastOriginBottomLeft.position.x;
        var endX = _raycastOriginBottomRight.position.x;
        var positionY = _raycastOriginBottomLeft.position.y;
        var incrementX = (endX - startX) / (_horizontalRaycastCount - 1);
        var raycastPositionX = startX;

        Gizmos.color = Color.red;

        for (var i = 0; i < _horizontalRaycastCount; i++)
        {
            var raycastOrigin = new Vector2(raycastPositionX, positionY);

            Gizmos.DrawLine(raycastOrigin, raycastOrigin + (Vector2.down * _raycastDistance));

            raycastPositionX += incrementX;
        }


        // gizmo for roof check
        startX = _raycastOriginTopLeft.position.x;
        endX = _raycastOriginTopRight.position.x;
        positionY = _raycastOriginTopLeft.position.y;
        incrementX = (endX - startX) / (_horizontalRaycastCount - 1);
        raycastPositionX = startX;

        Gizmos.color = Color.blue;

        for (var i = 0; i < _horizontalRaycastCount; i++)
        {
            var raycastOrigin = new Vector2(raycastPositionX, positionY);

            Gizmos.DrawLine(raycastOrigin, raycastOrigin + (Vector2.up * _raycastDistance));

            raycastPositionX += incrementX;
        }

        // gizmo for wall check left
        var startY = _raycastOriginBottomRight.position.y;
        var endY = _raycastOriginTopRight.position.y;
        var positionX = _raycastOriginTopLeft.position.x;
        var incrementY = (endY - startY) / (_verticalRaycastCount - 1);
        var raycastPositionY = startY;

        Gizmos.color = Color.green;

        for (var i = 0; i < _verticalRaycastCount; i++)
        {
            var raycastOrigin = new Vector2(positionX, raycastPositionY);

            Gizmos.DrawLine(raycastOrigin, raycastOrigin + (Vector2.left * _raycastDistance));

            raycastPositionY += incrementY;
        }

        // gizmo for wall check right
        startY = _raycastOriginBottomRight.position.y;
        endY = _raycastOriginTopRight.position.y;
        positionX = _raycastOriginTopRight.position.x;
        incrementY = (endY - startY) / (_verticalRaycastCount - 1);
        raycastPositionY = startY;

        Gizmos.color = Color.green;

        for (var i = 0; i < _verticalRaycastCount; i++)
        {
            var raycastOrigin = new Vector2(positionX, raycastPositionY);

            Gizmos.DrawLine(raycastOrigin, raycastOrigin + (Vector2.right * _raycastDistance));

            raycastPositionY += incrementY;
        }
    }
}
