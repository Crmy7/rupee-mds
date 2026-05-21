using UnityEngine;
using TMPro;

[RequireComponent(typeof(ScoreManager))]
[RequireComponent(typeof(TimeManager))]
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI remainingText;

    private ScoreManager _scoreManager;
    private TimeManager _timeManager;

    private void Awake()
    {
        _scoreManager = GetComponent<ScoreManager>();
        _timeManager = GetComponent<TimeManager>();
    }

    private void Update()
    {
        scoreText.text = $"Score : {_scoreManager.Score}";

        int totalSeconds = Mathf.CeilToInt(_timeManager.Remaining);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        remainingText.text = $"{minutes:00}:{seconds:00}";
    }
}
