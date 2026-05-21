using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
// Une rupee individuelle. Quand le joueur la touche, elle fire OnCollected puis se détruit.
public class Rupee : MonoBehaviour
{
    
    // Event écouté par RupeeManager pour tracker la collecte.
    public event Action<Rupee> OnCollected;

    private RupeeData _data;
    private SpriteRenderer _spriteRenderer;
    
    public RupeeData Data => _data;

    public void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(RupeeData data)
    {
        _data = data;
        _spriteRenderer.color = _data.color;
    }

    // Appelée automatiquement quand un autre Collider2D (en trigger) entre dans le trigger de cette rupee.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // On ignore tout sauf le joueur (tag "Player").
        if (!other.CompareTag("Player")) return;
        PlayPickupSound();

        OnCollected?.Invoke(this);
        Destroy(gameObject);
    }

    private void PlayPickupSound()
    {
        var audioGo = new GameObject("PickupSFX");
        audioGo.transform.position = transform.position;
        var source = audioGo.AddComponent<AudioSource>();
        source.clip = _data.pickupSound;
        source.spatialBlend = 0f;
        source.Play();
        Destroy(audioGo, _data.pickupSound.length);
    }
}
