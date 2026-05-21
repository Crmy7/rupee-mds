using UnityEngine;

// Gère uniquement la valeur du score. GameManager appelle IncrementScore/ResetScore.
public class ScoreManager : MonoBehaviour
{
    private int _score;

    // Propriété en lecture seule pour que l'UI affiche le score.
    public int Score => _score;

    public void IncrementScore()
    {
        _score++;
    }

    public void ResetScore()
    {
        _score = 0;
    }
}
