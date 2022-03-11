using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private GameObject stepPrefab;
    [SerializeField] private Vector3 stepsDistance;
    [SerializeField] private int stepsCount;
    [SerializeField] private Queue<Transform> steps = new Queue<Transform>();

    [SerializeField] private Vector3 movementDirection = new Vector3(1, -0.75f, 0);

    private float speed;
    private Vector3 destination;

    private void Start()
    {
        SetLadderSpeed();
        foreach (Transform step in transform)
        {
            steps.Enqueue(step);
        }
    }

    private void SetLadderSpeed()
    {
        speed = 2 * movementDirection.magnitude / GamePhysics.JumpTime;
    }

    public void OnBumbleJumped()
    {
        MoveLadder();
    }

    private void MoveLadder()
    {
        MoveFirstStepToTop();

        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        destination += movementDirection;

        while (destination.magnitude > 0.001f)
        {
            yield return new WaitForSeconds(Time.deltaTime);

            var md = Vector3.MoveTowards(Vector3.zero, destination, speed * Time.deltaTime);

            foreach (var step in steps)
            {
                step.position += md;
            }
            destination -= md;
        }
    }

    private void MoveFirstStepToTop()
    {
        var step = steps.Dequeue();
        step.transform.position += stepsDistance * steps.Count;
        steps.Enqueue(step);
    }
}
