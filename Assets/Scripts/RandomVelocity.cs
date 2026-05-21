using UnityEngine;

// Donne une direction aléatoire au Rigidbody2D au démarrage et maintient
// la vitesse constante (utilisé par les rupees et le spawner mobile).
[RequireComponent(typeof(Rigidbody2D))]
public class RandomVelocity : MonoBehaviour
{
    [SerializeField, Range(1f, 50)]
    private float speed = 8f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Vecteur aléatoire dans le plan 2D, normalisé pour que sa longueur soit toujours = 1.
        var direction = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
            ).normalized;

        _rb.linearVelocity = direction * speed;
    }

    // FixedUpdate : on re-normalise la vitesse à chaque step physique pour qu'elle reste constante,
    // même après un rebond (qui pourrait sinon la faire varier).
    private void FixedUpdate()
    {
        _rb.linearVelocity = _rb.linearVelocity.normalized * speed;
    }
}
