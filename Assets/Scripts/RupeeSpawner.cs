using UnityEngine;
using System.Collections;

public class RupeeSpawner : MonoBehaviour
{
    [SerializeField] private Rupee rupePrefab;
    
    [SerializeField] private Transform container;
    
    [SerializeField, Range(0.1f, 5f)] private float spawnDelay = 1f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Instantiate(rupePrefab, transform.position, Quaternion.identity, container);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
