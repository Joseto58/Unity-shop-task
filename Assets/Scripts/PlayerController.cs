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
    //Should now detect if player is interacting with something before activating the UI popup and changing the aactionMap

    SwitchActionMap(ActionMaps.UI);
  }


  private void OnEnable() {
    _playerControls.Movement.Enable();
  }

  private void OnDisable() {
    _playerControls.Movement.Disable();
  }


  public void SwitchActionMap(ActionMaps actionMap) {
    //Overkill for current implementation, but
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
