using System;
using UnityEngine;

// Une rupee individuelle. Quand le joueur la touche, elle fire OnCollected puis se détruit.
public class Rupee : MonoBehaviour
{
    // Event écouté par RupeeManager pour tracker la collecte.
    public event Action<Rupee> OnCollected;

    // Appelée automatiquement quand un autre Collider2D (en trigger) entre dans le trigger de cette rupee.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // On ignore tout sauf le joueur (tag "Player").
        if (!other.CompareTag("Player")) return;

        OnCollected?.Invoke(this);
        Destroy(gameObject);
    }
}
