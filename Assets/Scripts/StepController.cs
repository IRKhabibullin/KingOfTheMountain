using System.Collections;
using UnityEngine;

public class StepController : MonoBehaviour
{
    [SerializeField] private Vector3 movementDirection = new Vector3(1, -0.75f, 0);
    [SerializeField] private float speed = 3f;

    public void Move()
    {
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        float step = speed * Time.deltaTime;
        Vector3 destination = transform.position + movementDirection;

        while (Vector3.Distance(transform.position, destination) > 0.001f)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
        }
    }
}
