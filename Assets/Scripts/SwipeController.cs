using UnityEngine;
using UnityEngine.EventSystems;

public enum SwipeDerection
{
    None,
    Up,
    Down,
    Left,
    Right
}

public class SwipeController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float _swipeSensetive = 1f;
    [SerializeField] private PlayerController playerController;
    public SwipeDerection currentSwipe;
    private Vector2 startPoint;
    private bool onBeginDrag = false;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        startPoint = eventData.position;
        onBeginDrag = true;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta;
        if ((Mathf.Abs(delta.x) > _swipeSensetive || Mathf.Abs(delta.y) > _swipeSensetive) && onBeginDrag)
        {
            CheckSwipe(eventData.position - startPoint);
            startPoint = eventData.position;
            onBeginDrag = false;
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (onBeginDrag)
        {
            CheckSwipe(eventData.position - startPoint);
            onBeginDrag = false;
        }
    }

    private void CheckSwipe(Vector2 delta)
    {
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > _swipeSensetive && currentSwipe != SwipeDerection.Left)
            {
                currentSwipe = SwipeDerection.Right;
                playerController.RotateSnakeHead(SwipeDerection.Right);
            }
            else if (delta.x < _swipeSensetive && currentSwipe != SwipeDerection.Right)
            {
                currentSwipe = SwipeDerection.Left;
                playerController.RotateSnakeHead(SwipeDerection.Left);
            }
        }
        else
        {
            if (delta.y > _swipeSensetive && currentSwipe != SwipeDerection.Down)
            {
                currentSwipe = SwipeDerection.Up;
                playerController.RotateSnakeHead(SwipeDerection.Up);
            }
            else if (delta.y < _swipeSensetive && currentSwipe != SwipeDerection.Up)
            {
                currentSwipe = SwipeDerection.Down;
                playerController.RotateSnakeHead(SwipeDerection.Down);
            }
        }
    }
}