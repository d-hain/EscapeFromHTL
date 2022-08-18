using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour {
    private Rigidbody2D _rb;
    private Vector2 _moveDirection;
    private Animator _animator;
    private int _animHorizontalID;
    private int _animVerticalID;
    private int _animHorizontalIdleID;
    private int _animVerticalIdleID;
    private int _animSpeedID;
    private PlayerInputActions _playerInputActions;

    public float moveSpeed;

    /// <summary>
    /// Gets the components for <see cref="_rb"/> and <see cref="_animator"/>. <br/>
    /// Sets the <see cref="_playerInputActions"/> variable. <br/>
    /// Activates the ActionMap for the Player and enables it <br/>
    /// Gets all the IDs for the animation parameters
    /// </summary>
    private void Awake() {
        _playerInputActions = new PlayerInputActions();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animHorizontalID = Animator.StringToHash("Horizontal");
        _animVerticalID = Animator.StringToHash("Vertical");
        _animHorizontalIdleID = Animator.StringToHash("HorizontalIdle");
        _animVerticalIdleID = Animator.StringToHash("VerticalIdle");
        _animSpeedID = Animator.StringToHash("Speed");
        
        _playerInputActions.Player.Enable();
    }
    
    /// <summary>
    /// Enables the Movement Input
    /// </summary>
    private void OnEnable() {
        _playerInputActions.Player.Move.Enable();
    }

    /// <summary>
    /// Disables the Movement Input
    /// </summary>
    private void OnDisable() {
        _playerInputActions.Player.Move.Disable();
    }

    private void Update() {
        // Process movement input
        ProcessInputs();

        // Update the animator parameters
        if(_moveDirection.x != 0) {
            _animator.SetFloat(_animVerticalIdleID, 0);
            _animator.SetFloat(_animHorizontalIdleID, _moveDirection.x);
        }

        if(_moveDirection.y != 0) {
            _animator.SetFloat(_animHorizontalIdleID, 0);
            _animator.SetFloat(_animVerticalIdleID, _moveDirection.y);
        }

        _animator.SetFloat(_animHorizontalID, _moveDirection.x);
        _animator.SetFloat(_animVerticalID, _moveDirection.y);
        _animator.SetFloat(_animSpeedID, _moveDirection.sqrMagnitude);
    }

    void FixedUpdate() {
        Move();
    }

    /// <summary>
    /// Process all inputs for the player movement <br />
    /// Sets <see cref="_moveDirection"/> to the input value
    /// </summary>
    private void ProcessInputs() {
        _moveDirection = _playerInputActions.Player.Move.ReadValue<Vector2>();
    }

    /// <summary>
    /// Moves the player in the direction of <see cref="_moveDirection"/>
    /// </summary>
    private void Move() {
        _rb.velocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
    }
}