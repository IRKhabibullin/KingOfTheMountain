using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Bumble : MonoBehaviour
{
    public UnityEvent bumbleJumped;

    [SerializeField] private Vector3 moveUpForce;
    [SerializeField] private Vector3 moveSideForce;

    private Rigidbody _rb;
    private float lastJumpedTime;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        SetJumpVelocity();
    }

    private void SetJumpVelocity()
    {
        moveUpForce = new Vector3(0, Mathf.Sqrt(Mathf.Abs(2 * Physics.gravity.y * GamePhysics.JumpHeight)), 0);
        moveSideForce = new Vector3(0, moveUpForce.y * 0.8f, 1);
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

        _rb.velocity = new Vector3(moveSideForce.x, moveSideForce.y, moveSideForce.z * direction);
        lastJumpedTime = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
    }
}
