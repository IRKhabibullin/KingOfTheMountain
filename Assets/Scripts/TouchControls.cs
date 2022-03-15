using UnityEngine;
using UnityEngine.Events;

public class TouchControls : MonoBehaviour
{
    [SerializeField] private int minSwipeDistance;

    public UnityEvent<int> OnSwipe;
    public UnityEvent OnTap;

    private Vector2 touchStartPosition;
    private Vector2 touchEndPosition;
    
    void Update()
    {
        if (GameStateMachine.Instance.currentState != "Play") return;

        if (Input.touchCount == 0) return;

        // track only first touch for simplicity
        var touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            touchStartPosition = touch.position;
        }
        if (IsStartDefined() && touch.phase == TouchPhase.Ended)
        {
            touchEndPosition = touch.position;
            DetectValidInput();
            ResetTouch();
        }
    }

    private bool IsStartDefined()
    {
        return touchStartPosition != Vector2.negativeInfinity;
    }

    private void ResetTouch()
    {
        touchStartPosition = Vector2.negativeInfinity;
    }

    private void DetectValidInput()
    {
        var xDelta = touchEndPosition.x - touchStartPosition.x;

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
