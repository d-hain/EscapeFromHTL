using Unity.VisualScripting;
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
    
    public float moveSpeed;

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
        //TODO: still playing idle anim when moving
        if(_moveDirection.x != 0) {
            _animator.SetFloat(_animHorizontalIdleID, _moveDirection.x);
        }
        if(_moveDirection.y != 0) {
            _animator.SetFloat(_animVerticalIdleID, _moveDirection.y);
        }
        _animator.SetFloat(_animHorizontalID, _moveDirection.x);
        _animator.SetFloat(_animVerticalID, _moveDirection.y);
        _animator.SetFloat(_animSpeedID, _moveDirection.sqrMagnitude);
    }

    void FixedUpdate() {
        Move();
    }

    private void ProcessInputs() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void Move() {
        _rb.velocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
        SetAnimMoveSpeed(_rb.velocity);
    }

    private void SetAnimMoveSpeed(Vector2 velocity) { }
}