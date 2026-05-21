using UnityEngine;
using TMPro;

[RequireComponent(typeof(ScoreManager))]
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private ScoreManager _scoreManager;
    
    private void Awake()
    {
        _scoreManager = GetComponent<ScoreManager>();
    }

    private void Update()
    {
        scoreText.text = $"Score : {_scoreManager.Score}";
    }
}
