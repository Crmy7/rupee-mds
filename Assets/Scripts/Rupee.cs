using System;
using UnityEngine;

public class Rupee : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log($"Rupee collected at {transform.position}");
        Destroy(gameObject);
    }
}