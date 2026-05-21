using System;
using UnityEngine;

// Orchestrateur central : démarre/arrête la partie et écoute les events des autres managers.
[RequireComponent(typeof(TimeManager))]
[RequireComponent(typeof(RupeeManager))]
[RequireComponent(typeof(ScoreManager))]
public class GameManager : MonoBehaviour
{
    // Références aux autres managers du même GameObject (récupérées dans Awake).
    private TimeManager _timeManager;
    private RupeeManager _rupeeManager;
    private ScoreManager _scoreManager;
    
    [SerializeField]
    private PlayerController playerController;

    // Events écoutés par l'UIManager (cache/affiche le bouton Start).
    public event Action OnGameStarted;
    public event Action OnGameStopped;

    private void Awake()
    {
        _timeManager = GetComponent<TimeManager>();
        _rupeeManager = GetComponent<RupeeManager>();
        _scoreManager = GetComponent<ScoreManager>();
    }

    // Appelée par le bouton Start de l'UI (via son OnClick).
    public void StartGame()
    {
        // Reset tout avant de relancer une partie.
        _timeManager.ResetTimer();
        _rupeeManager.ResetRupees();
        _scoreManager.ResetScore();

        _timeManager.StartTimer();
        _rupeeManager.StartSpawning();
        playerController.SetCanMove(true);

        OnGameStarted?.Invoke();
    }

    public void StopGame()
    {
        playerController.SetCanMove(false);
        _rupeeManager.StopSpawning();
        _rupeeManager.ResetRupees();
        
        OnGameStopped?.Invoke();
    }

    // OnEnable / OnDisable : on s'abonne et désabonne aux events pour éviter les fuites mémoire.
    private void OnEnable()
    {
        _timeManager.OnTimeUp += HandleTimeUp;
        _rupeeManager.OnRupeeCollected += HandleRupeeCollected;
    }

    private void OnDisable()
    {
        _timeManager.OnTimeUp -= HandleTimeUp;
        _rupeeManager.OnRupeeCollected -= HandleRupeeCollected;
    }

    private void HandleRupeeCollected(Rupee rupee)
    {
        _scoreManager.IncrementScore();
    }

    // Temps écoulé → on arrête la partie (stop spawn + fire OnGameStopped).
    private void HandleTimeUp()
    {
        StopGame();
    }
}
