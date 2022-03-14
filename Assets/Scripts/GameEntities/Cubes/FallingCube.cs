using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingCube : MonoBehaviour
{
    [SerializeField] private Vector3 jumpForce;
    [SerializeField] private float jumpTimeout;
    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpTime;

    private Rigidbody _rb;
    private IEnumerator jumpCoroutine;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        SetJumpVelocity();
    }

    private void SetJumpVelocity()
    {
        // Some magic number, needed because of difference in height of jump start and end points. Found experimentally
        float xVelocityCoefficient = 1.28f;
        jumpForce = new Vector3(jumpLength / (xVelocityCoefficient * jumpTime), (jumpLength - 0.5f * Physics.gravity.y * Mathf.Pow(jumpTime, 2f)) / jumpTime, 0);
    }

    private IEnumerator JumpCoroutine()
    {
        yield return new WaitForSeconds(jumpTimeout);

        _rb.AddForce(jumpForce, ForceMode.VelocityChange);
        jumpCoroutine = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ladder") && jumpCoroutine == null)
        {
            jumpCoroutine = JumpCoroutine();
            StartCoroutine(jumpCoroutine);
        }
    }
}
