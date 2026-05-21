using UnityEngine;

// Gère uniquement la valeur du score. GameManager appelle IncrementScore/ResetScore.
public class ScoreManager : MonoBehaviour
{
    private int _score;
    private int _bestScore;

    // Propriété en lecture seule pour que l'UI affiche le score.
    public int Score => _score;
    public int BestScore => _bestScore;

    private void Awake()
    {
        _bestScore = PlayerPrefs.GetInt("BestScoreKey", 0);
    }
    
    public void IncrementScore()
    {
        _score++;
        TrySaveBestScore();
    }

    public void TrySaveBestScore()
    {
        if (_score > _bestScore)
        {
            _bestScore = _score;
            PlayerPrefs.SetInt("BestScoreKey", _score);
            PlayerPrefs.Save();
        }
    }

    public void ResetScore()
    {
        _score = 0;
    }
}
