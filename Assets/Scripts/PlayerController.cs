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
  Vector2 _direction;
  private AnimatorClipInfo[] _clipInfo;
  [SerializeField] Animation _thoughtAnimation;


  LayerMask _interactionLayer;

  float _walkingSpeed = 3f;

  Vector2 _currentMovementInput;

  private void Awake() {
    _playerControls = new PlayerControls();
    _animator = GetComponent<Animator>();

    _rigidbody = GetComponent<Rigidbody2D>();
    _interactionLayer = LayerMask.GetMask("Interaction");
    
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
    if (CheckInteraction()) {
      //SwitchActionMap(ActionMaps.UI);
      print("Interacting!");
    } else {
      PlayThoughtBubble();
    }
  }

  void PlayThoughtBubble() {
    _thoughtAnimation.Play();
  }

  bool CheckInteraction() {
    Debug.DrawLine(transform.position, (Vector2)transform.position+GetPlayerSpriteDirection(), Color.red, 1f);
    return (Physics2D.Raycast(transform.position, GetPlayerSpriteDirection(), 1f,_interactionLayer));
  }

  //With this approach we can get which direction the sprite itself is facing, independent of how the rigidbody is behaving.
  private Vector2 GetPlayerSpriteDirection() {

    _clipInfo = _animator.GetCurrentAnimatorClipInfo(0);

    switch (_clipInfo[0].clip.name) {
      case string name when name.Contains("Up"):
        return Vector2.up;
      case string name when name.Contains("Down"):
        return Vector2.down;
      case string name when name.Contains("Left"):
        return Vector2.left;
      case string name when name.Contains("Right"):
        return Vector2.right;
      default:
        Debug.Log("Invalid state");
        return Vector2.zero;
    }
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
        Debug.LogWarning("actionMap not implemented!");
        break;
    }
  }


  private void OnEnable() {
    _playerControls.Movement.Enable();
  }

  private void OnDisable() {
    _playerControls.Movement.Disable();
  }

}
