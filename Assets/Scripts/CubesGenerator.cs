using System.Collections;
using UnityEngine;

public class CubesGenerator : MonoBehaviour
{
    [SerializeField] Transform ladder;
    [SerializeField] GameObject stepPrefab;
    [SerializeField] GameObject cubePrefab;
    [SerializeField] Transform generationPoint;
    [SerializeField] float generationTimeout;

    private void Start()
    {
        StartCoroutine(GenerateCoroutine());
    }

    private IEnumerator GenerateCoroutine()
    {
        while (true) {

            GenerateCube();

            yield return new WaitForSeconds(generationTimeout);
        }
    }

    private void GenerateCube()
    {
        var stepWidthHalf = stepPrefab.transform.localScale.z / 2;
        var cubePosition = new Vector3(generationPoint.position.x, generationPoint.position.y, Random.Range(-stepWidthHalf, stepWidthHalf));

        var cube = Instantiate(cubePrefab, cubePosition, Quaternion.identity);
        cube.transform.parent = ladder;
    }
}
