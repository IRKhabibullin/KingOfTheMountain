using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private GameObject stepPrefab;
    [Range(0, 1)] [SerializeField] private float sideJumpPower = 0.6f; // ratio to normal jump power

    private Rigidbody _rb;
    private Bumble bumble;

    public UnityEvent bumbleJumped;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        bumble = new Bumble(sideJumpPower, stepPrefab.transform.localScale.z);
    }

    public void ResetPosition()
    {
        StopMovement();
        transform.position = startPosition;
    }

    private void StopMovement()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    public void Jump()
    {
        if (!bumble.CanJump) return;

        _rb.AddForce(bumble.jumpForce, ForceMode.VelocityChange);
        bumble.UpdateJumpTime();
        bumbleJumped?.Invoke();
    }

    public void JumpAside(int direction)
    {
        if (!bumble.CanJump) return;

        if (!bumble.CanJumpAside(direction)) return;

        _rb.AddForce(bumble.GetSideJumpForce(direction), ForceMode.VelocityChange);
        bumble.UpdateSideJump(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            StopMovement();
        }
        else if (collision.gameObject.CompareTag("Cube"))
        {
            GameStateMachine.Instance.ChangeState("Game over");
        }
    }
}
