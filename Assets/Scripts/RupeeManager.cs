using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawn des rupees à intervalle régulier + tracking des rupees actives.
// Fire OnRupeeCollected quand une rupee est ramassée par le joueur.
public class RupeeManager : MonoBehaviour
{
    [SerializeField] private Transform spawner;       // Point d'apparition (un GameObject mobile dans la scène)
    [SerializeField] private Rupee rupeePrefab;       // Prefab à instancier
    [SerializeField] private Transform container;     // Parent dans la hiérarchie où ranger les rupees

    [SerializeField, UnityEngine.Range(0.1f, 5f)]
    private float spawnDelay = 1f;

    // Event écouté par GameManager pour incrémenter le score.
    public event Action<Rupee> OnRupeeCollected;

    public readonly List<Rupee> _rupees = new();
    private Coroutine _spawnRoutine;

    // Détruit toutes les rupees encore en scène et stoppe la coroutine.
    public void ResetRupees()
    {
        StopSpawning();
        foreach (var rupee in _rupees)
        {
            if (rupee != null) Destroy(rupee.gameObject);
        }
        _rupees.Clear();
    }

    public void StartSpawning()
    {
        _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
    }

    // Coroutine : boucle infinie qui spawn puis attend spawnDelay secondes.
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

    // On s'abonne à l'event OnCollected de chaque rupee pour être notifié de sa collecte.
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
