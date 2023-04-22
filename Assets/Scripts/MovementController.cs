using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MovementController : MonoBehaviour
{

  PlayerControls _playerControls;
  BoxCollider2D _collider;
  Rigidbody2D _rigidbody;

  float _walkingSpeed = 3f;

  Vector2 _currentMovementInput;

  private void Awake() {
    _playerControls = new PlayerControls();

    //_collider = GetComponent<BoxCollider2D>();
    _rigidbody = GetComponent<Rigidbody2D>();
    _playerControls.Movement.move.started += OnMove;
    _playerControls.Movement.move.performed += OnMove;
    _playerControls.Movement.move.canceled += OnMove;

    _playerControls.Movement.move.started += OnMove;
    _playerControls.Movement.interact.started += OnInteract;
  }

  // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  private void OnMove(InputAction.CallbackContext context) {
    _currentMovementInput = context.ReadValue<Vector2>();
    _rigidbody.velocity = _currentMovementInput*_walkingSpeed;
  }

  private void OnInteract(InputAction.CallbackContext context) {
    print("Interacting");
  }


  private void OnEnable() {
    _playerControls.Movement.Enable();
  }

  private void OnDisable() {
    _playerControls.Movement.Disable();
  }
}
