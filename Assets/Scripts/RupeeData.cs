using UnityEngine;

[CreateAssetMenu(fileName = "RupeeData", menuName = "Rupees/RupeeData")]
public class RupeeData : ScriptableObject
{
    public Color color = Color.green;
    public int score = 1;
    public AudioClip pickupSound;
}
