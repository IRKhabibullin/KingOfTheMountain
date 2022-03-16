using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Vector3 stepDistance = new Vector3(1, -0.75f, 0);
    [SerializeField] private Queue<Transform> steps = new Queue<Transform>();

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

    public void ResetStepsPosition()
    {
        int i = 1;
        foreach (var step in steps)
        {
            step.localPosition = -stepDistance * i++;
        }
    }

    private void SetLadderSpeed()
    {
        speed = 2 * stepDistance.magnitude / GamePhysics.JumpTime;
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
        destination += stepDistance;

        while (destination.magnitude > 0.001f)
        {
            yield return new WaitForSeconds(Time.deltaTime);

            var md = Vector3.MoveTowards(Vector3.zero, destination, speed * Time.deltaTime);

            foreach (Transform step in transform)
            {
                step.position += md;
            }
            destination -= md;
        }
    }

    private void MoveFirstStepToTop()
    {
        var step = steps.Dequeue();
        step.transform.position -= stepDistance * steps.Count;
        steps.Enqueue(step);
    }
}
