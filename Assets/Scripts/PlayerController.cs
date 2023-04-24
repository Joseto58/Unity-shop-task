using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ActionMaps
{
  Movement,
  UI
}

public class PlayerController : MonoBehaviour
{
  
  PlayerControls _playerControls;
  BoxCollider2D _collider;
  Rigidbody2D _rigidbody;
  Animator _animator;


  //Animation
  bool _isWalking = false;
  Vector2 _direction;

  float _walkingSpeed = 3f;

  Vector2 _currentMovementInput;

  private void Awake() {
    _playerControls = new PlayerControls();
    _animator = GetComponent<Animator>();

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
    HandleAnimation();
    }
  
  void HandleAnimation() {
/*
    if (_direction != Vector2.zero) {
      _isWalking = true;
    } else {
      _isWalking = false;
    }
  */
    _animator.SetInteger("directionX", (int)(_direction.x*2f));
    _animator.SetInteger("directionY", (int)(_direction.y*2f));
  }

  private void OnMove(InputAction.CallbackContext context) {
    _currentMovementInput = context.ReadValue<Vector2>();
    _rigidbody.velocity = _currentMovementInput*_walkingSpeed;
    _direction = _currentMovementInput;

  }

  private void OnInteract(InputAction.CallbackContext context) {
    //Should now detect if player is interacting with something before activating the UI popup and changing the actionMap
    //Check for interaction, if false, trigger a ? bubble
    //If true, trigger interaction and give control over to the UI action map
    if(CheckInteraction())
    SwitchActionMap(ActionMaps.UI);
  }

  bool CheckInteraction() {
    return false;
  }

  private void OnEnable() {
    _playerControls.Movement.Enable();
  }

  private void OnDisable() {
    _playerControls.Movement.Disable();
  }


    //Overkill for current implementation, but cleaner than using an int or a string.
  public void SwitchActionMap(ActionMaps actionMap) {
    switch (actionMap) {
      case ActionMaps.Movement:
        _playerControls.UI.Disable();
        _playerControls.Movement.Enable();
        break;
      case ActionMaps.UI:
        _playerControls.Movement.Disable();
        _playerControls.UI.Enable();
        break;
      default:
        break;
    }
  }

}
