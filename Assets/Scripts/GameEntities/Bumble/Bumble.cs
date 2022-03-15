using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Bumble : MonoBehaviour
{
    public UnityEvent bumbleJumped;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 moveUpForce;
    [SerializeField] private Vector3 moveSideForce;
    [SerializeField] private GameObject stepPrefab;

    private Rigidbody _rb;
    private float lastJumpedTime;
    private int maxSideJumps;
    private int currentSideJump = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        SetJumpVelocity();
        SetSideJumpsLimit();
    }

    private void SetJumpVelocity()
    {
        moveUpForce = new Vector3(0, Mathf.Sqrt(Mathf.Abs(2 * Physics.gravity.y * GamePhysics.JumpHeight)), 0);
        moveSideForce = new Vector3(0, moveUpForce.y * 0.6f, 1);
    }

    private void SetSideJumpsLimit()
    {
        var stepWidthHalf = stepPrefab.transform.localScale.z / 2;
        var jumpTime = -2 * moveSideForce.y / Physics.gravity.y;
        var sideJumpDistance = moveSideForce.z * jumpTime;
        maxSideJumps = (int)(stepWidthHalf / sideJumpDistance);
    }

    public void ResetObject()
    {
        // TODO probably need to move it all to simple class and rename this to just Reset
        transform.position = startPosition;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    public void Jump()
    {
        if (Time.time - lastJumpedTime < GamePhysics.JumpTime) return;

        _rb.velocity = moveUpForce;
        lastJumpedTime = Time.time;
        bumbleJumped?.Invoke();
    }

    public void MoveSide(int direction)
    {
        if (Time.time - lastJumpedTime < GamePhysics.JumpTime) return;

        if (Mathf.Abs(currentSideJump + direction) > maxSideJumps) return;

        _rb.velocity = new Vector3(moveSideForce.x, moveSideForce.y, moveSideForce.z * direction);
        currentSideJump += direction;
        lastJumpedTime = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
        else if (collision.gameObject.CompareTag("Cube"))
        {
            GameStateMachine.Instance.ChangeState("Game over");
        }
    }
}
