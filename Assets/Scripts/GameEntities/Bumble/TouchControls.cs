using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchControls : MonoBehaviour
{
    [SerializeField] private int minSwipeDistance;

    public UnityEvent<int> OnSwipe;
    public UnityEvent OnTap;

    private Vector2? touchStartPosition = null;
    private Vector2 touchEndPosition;
    
    void Update()
    {
        if (Input.touchCount == 0) return;

        // track only first touch for simplicity
        var touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            if (!IsTouchValid(touch))
            {
                ResetStartTouch();
                return;
            }

            touchStartPosition = touch.position;
        }
        if (IsStartTouchDefined() && touch.phase == TouchPhase.Ended)
        {
            touchEndPosition = touch.position;
            DetectValidInput();
            ResetStartTouch();
        }
    }

    /// <summary>
    /// Check that touch is not over UI
    /// </summary>
    /// <param name="touch"></param>
    /// <returns></returns>
    private bool IsTouchValid(Touch touch)
    {
        return !EventSystem.current.IsPointerOverGameObject(touch.fingerId);
    }

    private void ResetStartTouch()
    {
        touchStartPosition = null;
    }

    private bool IsStartTouchDefined()
    {
        return touchStartPosition != null;
    }

    private void DetectValidInput()
    {
        var xDelta = touchEndPosition.x - ((Vector2)touchStartPosition).x;

        if (Mathf.Abs(xDelta) < minSwipeDistance)
        {
            OnTap?.Invoke();
        }
        else
        {
            OnSwipe?.Invoke((int)Mathf.Sign(xDelta));
        }
    }
}
