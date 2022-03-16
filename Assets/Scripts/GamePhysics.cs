using UnityEngine;

/// <summary>
/// Physics calculus according to desired bumble jump height and time
/// </summary>
public class GamePhysics : MonoBehaviour
{
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpTime;

    public static float JumpHeight { get; private set; }
    public static float JumpTime { get; private set; }

    private void Awake()
    {
        JumpHeight = jumpHeight;
        JumpTime = jumpTime;

        SetGravity();
    }

    private void SetGravity()
    {
        Physics.gravity = new Vector3(0, -(2 * jumpHeight) / (Mathf.Pow(jumpTime / 2, 2)), 0);
    }
}
