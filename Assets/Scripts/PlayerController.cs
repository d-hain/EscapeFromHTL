using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private InputAction _move;
    private InputAction _interact;
    
    public float moveSpeed;
    public PlayerInputActions playerControls;

    private void Awake() {
        playerControls = new PlayerInputActions();
    }
    
    /// <summary>
    /// Gets the components for <see cref="_rb"/> and <see cref="_animator"/>
    /// Gets all the IDs for the animation parameters
    /// </summary>
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animHorizontalID = Animator.StringToHash("Horizontal");
        _animVerticalID = Animator.StringToHash("Vertical");
        _animHorizontalIdleID = Animator.StringToHash("HorizontalIdle");
        _animVerticalIdleID = Animator.StringToHash("VerticalIdle");
        _animSpeedID = Animator.StringToHash("Speed");
    }

    private void Update() {
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

    private void OnEnable() {
        _move = playerControls.Player.Move;
        _move.Enable();
        
        _interact = playerControls.Player.Interact;
        _interact.Enable();
        _interact.performed += OnInteract;
    }

    private void OnDisable() {
        _move.Disable();
        _interact.Disable();
    }

    /// <summary>
    /// Process all inputs for the player movement
    /// Sets <see cref="_moveDirection"/> to the input value
    /// </summary>
    private void ProcessInputs() {
        _moveDirection = _move.ReadValue<Vector2>();
    }

    /// <summary>
    /// Moves the player in the direction of <see cref="_moveDirection"/>
    /// </summary>
    private void Move() {
        _rb.velocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
    }

    public void OnInteract(InputAction.CallbackContext value) {
        Debug.Log("Interact");
    }
}