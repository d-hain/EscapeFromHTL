using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;

    private Rigidbody2D _rb;
    private Vector2 _moveDirection;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        ProcessInputs();
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
    }
}