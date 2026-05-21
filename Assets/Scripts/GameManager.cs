using System;
using UnityEngine;

[RequireComponent(typeof(TimeManager))]
[RequireComponent(typeof(RupeeManager))]
[RequireComponent(typeof(ScoreManager))]
public class GameManager : MonoBehaviour
{
    private TimeManager _timeManager;
    private RupeeManager _rupeeManager;
    private ScoreManager _scoreManager;

    private void Awake()
    {
        _timeManager = GetComponent<TimeManager>();
        _rupeeManager = GetComponent<RupeeManager>();
        _scoreManager = GetComponent<ScoreManager>();
    }

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

    private void HandleTimeUp()
    {
        _rupeeManager.StopSpawning();
    }
}
