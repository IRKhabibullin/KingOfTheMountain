using System.Collections;
using UnityEngine;

public class CubesGenerator : MonoBehaviour
{
    [SerializeField] private Transform ladder;
    [SerializeField] private GameObject stepPrefab;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform generationPoint;
    [SerializeField] private float generationTimeout;
    private IEnumerator generationCoroutine;

    public void EnableGeneration()
    {
        if (generationCoroutine != null) return;

        generationCoroutine = GenerateCoroutine();
        StartCoroutine(generationCoroutine);
    }

    public void DisableGeneration()
    {
        if (generationCoroutine == null) return;

        StopCoroutine(generationCoroutine);
        generationCoroutine = null;
    }

    public void DestroyAllCubes()
    {
        foreach(var cube in FindObjectsOfType<FallingCube>())
        {
            Destroy(cube.gameObject);
        }
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
