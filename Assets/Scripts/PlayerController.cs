using System;
using UnityEngine;
using UnityEngine.InputSystem;

// [RequireComponent] garantit qu'un Rigidbody2D est présent sur le GameObject.
// Si tu ajoutes ce script à un objet sans Rigidbody2D, Unity en créera un automatiquement.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // [SerializeField] rend le champ visible dans l'inspecteur Unity, même s'il est privé.
    // [Range] crée un curseur dans l'inspecteur pour limiter la valeur entre 1 et 20.
    [SerializeField, Range(1f, 20f)]
    private float speed = 5f;

    // Référence au Rigidbody2D du joueur (utilisé pour gérer la physique et le déplacement).
    private Rigidbody2D _rb;

    // Stocke la direction de déplacement reçue depuis l'Input System (clavier, manette, etc.).
    private Vector2 _movementInput;

    // Awake() est appelée une seule fois, dès que l'objet est créé dans la scène,
    // avant Start(). C'est l'endroit idéal pour récupérer les références aux composants.
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // OnMove() est automatiquement appelée par le composant PlayerInput
    // quand l'action "Move" est déclenchée (touches ZQSD/WASD, joystick, etc.).
    // Le nom de la méthode doit correspondre à l'action définie dans les Input Actions.
    void OnMove(InputValue value)
    {
        // On récupère la valeur de l'input sous forme de Vector2 (x = horizontal, y = vertical).
        _movementInput = value.Get<Vector2>();
    }

    // FixedUpdate() est appelée à intervalle fixe (par défaut toutes les 0.02s),
    // indépendamment du framerate. C'est là qu'il faut faire les calculs de physique.
    private void FixedUpdate()
    {
        // .normalized garantit que la vitesse reste constante dans toutes les directions
        // (sinon le déplacement en diagonale serait plus rapide qu'à l'horizontale ou la verticale).
        // linearVelocity définit directement la vélocité du Rigidbody2D.
        _rb.linearVelocity = _movementInput.normalized * speed;
    }
}
