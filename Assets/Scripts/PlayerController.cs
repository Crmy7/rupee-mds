using System;
using UnityEngine;
using UnityEngine.InputSystem;

// [RequireComponent] garantit qu'un Rigidbody2D et un Animator sont présents sur le GameObject.
// Si tu ajoutes ce script à un objet sans ces composants, Unity les créera automatiquement.
// L'Animator est nécessaire pour piloter les animations (idle/walk dans les 4 directions).
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // [SerializeField] rend le champ visible dans l'inspecteur Unity, même s'il est privé.
    // [Range] crée un curseur dans l'inspecteur pour limiter la valeur entre 1 et 20.
    [SerializeField, Range(1f, 100)]
    private float speed = 5f;

    // Référence au Rigidbody2D du joueur (utilisé pour gérer la physique et le déplacement).
    private Rigidbody2D _rb;

    // Stocke la direction de déplacement reçue depuis l'Input System (clavier, manette, etc.).
    private Vector2 _movementInput;
    
    // Référence à l'Animator du joueur (utilisé pour transmettre la direction
    // aux paramètres "Horizontal" et "Vertical" du Blend Tree, qui choisira
    // automatiquement la bonne animation selon la direction).
    private Animator _animator;

    // 
    private bool _canMove;
    
    // Awake() est appelée une seule fois, dès que l'objet est créé dans la scène,
    // avant Start(). C'est l'endroit idéal pour récupérer les références aux composants.
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        // GetComponent<Animator>() récupère le composant Animator attaché au même GameObject.
        _animator = GetComponent<Animator>();
    }

    public void SetCanMove(bool value)
    {
        _canMove = value;
        if(!value) _movementInput = Vector2.zero;
    }

    // OnMove() est automatiquement appelée par le composant PlayerInput
    // quand l'action "Move" est déclenchée (touches ZQSD/WASD, joystick, etc.).
    // Le nom de la méthode doit correspondre à l'action définie dans les Input Actions.
    void OnMove(InputValue value)
    {
        if (!_canMove)  return;
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

        // SetFloat() envoie une valeur à un paramètre de l'Animator.
        // Ces paramètres "Horizontal" et "Vertical" doivent être créés dans
        // l'Animator Controller (onglet Parameters) et utilisés dans un Blend Tree
        // pour choisir automatiquement l'animation correspondant à la direction
        // (ex : Horizontal=1 → walk-est, Vertical=-1 → walk-south).
        _animator.SetFloat("Horizontal", _movementInput.x);
        _animator.SetFloat("Vertical", _movementInput.y);
        _animator.SetFloat("Velocity", _movementInput.sqrMagnitude);

        if (_movementInput.sqrMagnitude > 0.01f)
        {
            _animator.SetFloat("LastHorizontal", _movementInput.x);
            _animator.SetFloat("LastVertical", _movementInput.y);
        }
    }
}
