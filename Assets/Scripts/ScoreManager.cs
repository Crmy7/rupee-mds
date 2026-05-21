using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _score;
    public int Score => _score;

    public void IncrementScore()
    {
        _score++;
    }
}
