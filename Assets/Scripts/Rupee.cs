using System;
using UnityEngine;

public class Rupee : MonoBehaviour
{
    public event Action<Rupee> OnCollected;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Debug.Log($"Rupee collected at {transform.position}");
        OnCollected?.Invoke(this);
        Destroy(gameObject);
    }
}