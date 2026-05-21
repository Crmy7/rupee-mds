using UnityEngine;
using System;

// Décompteur de temps. Fire OnTimeUp quand _remaining atteint 0.
public class TimeManager : MonoBehaviour
{
    // [Range] crée un slider dans l'Inspector entre 10s et 600s.
    [SerializeField, Range(10f, 600f)] private float duration = 120f;

    // Event écouté par GameManager pour savoir quand le temps est écoulé.
    public event Action OnTimeUp;

    private float _remaining;
    private bool _running;

    // Propriété en lecture seule pour que l'UI puisse afficher le temps restant.
    public float Remaining => _remaining;

    private void Start()
    {
        // Initialise l'affichage avant le premier StartTimer.
        _remaining = duration;
    }

    public void StartTimer()
    {
        _running = true;
    }

    public void ResetTimer()
    {
        _remaining = duration;
        _running = false;
    }

    private void Update()
    {
        // Si la partie n'est pas en cours, on ne décompte pas.
        if (!_running) return;

        _remaining -= Time.deltaTime;

        if (_remaining <= 0f)
        {
            _remaining = 0f;
            _running = false;
            OnTimeUp?.Invoke();
        }
    }
}
