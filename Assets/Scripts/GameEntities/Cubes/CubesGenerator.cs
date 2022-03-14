using System.Collections;
using UnityEngine;

public class CubesGenerator : MonoBehaviour
{
    [SerializeField] Transform ladder;
    [SerializeField] GameObject stepPrefab;
    [SerializeField] GameObject cubePrefab;
    [SerializeField] Transform generationPoint;
    [SerializeField] float generationTimeout;
    [SerializeField] bool isGenerating;

    private void Start()
    {
        StartCoroutine(GenerateCoroutine());
    }

    public void EnableGeneration()
    {
        isGenerating = true;
    }

    public void DisableGeneration()
    {
        isGenerating = false;
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
            if (isGenerating)
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
