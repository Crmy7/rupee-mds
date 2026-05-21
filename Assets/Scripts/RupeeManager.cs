using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupeeManager : MonoBehaviour
{
    [SerializeField] private Transform spawner;
    [SerializeField] private Rupee rupeePrefab;
    [SerializeField] private Transform container;
    [SerializeField, UnityEngine.Range(0.1f, 5f)] private float spawnDelay = 1f;

    public event Action<Rupee> OnRupeeCollected;

    public readonly List<Rupee> _rupees = new();

    private void Start()
    {
        StartSpawning();
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Spawn()
    {
        var rupee = Instantiate(rupeePrefab, spawner.position, Quaternion.identity, container);
        AddRupee(rupee);
    }

    private void AddRupee(Rupee rupee)
    {
        _rupees.Add(rupee);
        rupee.OnCollected += RupeeHandleRupeeCollected;
    }

    private void RupeeHandleRupeeCollected(Rupee rupee)
    {
        _rupees.Remove(rupee);
        rupee.OnCollected -= RupeeHandleRupeeCollected;
        OnRupeeCollected?.Invoke(rupee);
    }
    
    

}
