using System;
using UnityEngine;
using TMPro;

// Gère tout l'affichage à l'écran : score, temps restant, visibilité du bouton Start.
[RequireComponent(typeof(ScoreManager))]
[RequireComponent(typeof(TimeManager))]
[RequireComponent(typeof(GameManager))]
public class UIManager : MonoBehaviour
{
    // Champs assignés dans l'Inspector (drag & drop des éléments UI du Canvas).
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI remainingText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private GameObject startButton;

    private ScoreManager _scoreManager;
    private TimeManager _timeManager;
    private GameManager _gameManager;

    private void Awake()
    {
        _scoreManager = GetComponent<ScoreManager>();
        _timeManager = GetComponent<TimeManager>();
        _gameManager = GetComponent<GameManager>();
    }

    // On s'abonne aux events de GameManager pour cacher/réafficher le bouton.
    private void OnEnable()
    {
        _gameManager.OnGameStarted += HandleGameStarted;
        _gameManager.OnGameStopped += HandleGameStopped;
    }

    private void OnDisable()
    {
        _gameManager.OnGameStarted -= HandleGameStarted;
        _gameManager.OnGameStopped -= HandleGameStopped;
    }

    private void HandleGameStarted()
    {
        startButton.SetActive(false);
    }

    private void HandleGameStopped()
    {
        startButton.SetActive(true);
    }

    private void Update()
    {
        scoreText.text = $"Score : {_scoreManager.Score}";
        bestScoreText.text = $"Best : {_scoreManager.BestScore}";

        // TimeSpan.FromSeconds(...).ToString("mm\\:ss") → formate 90s en "01:30".
        remainingText.text = TimeSpan.FromSeconds(_timeManager.Remaining).ToString(@"mm\:ss");
    }
}
