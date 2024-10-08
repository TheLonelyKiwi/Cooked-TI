using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _rotateSpeed;

    [SerializeField] private Transform _visualTransform;

    public bool canMove { get; set; } = false;

    private Rigidbody _rigidbody;
    private Vector3 _currentInput;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate() {
        if (!canMove) return;
        
        Vector3 targetVelocity = _currentInput * _movementSpeed;
        Vector3 newVelocity = Vector3.Lerp(_rigidbody.velocity, targetVelocity, 1 - Mathf.Exp(-_acceleration * Time.fixedDeltaTime));
        _rigidbody.velocity = newVelocity;

        if (Vector3.Magnitude(_currentInput) > 0) {
            Quaternion targetRotation = Quaternion.LookRotation(_currentInput);
            Quaternion newRotation = Quaternion.Slerp(_visualTransform.rotation, targetRotation, 1 - Mathf.Exp(-_rotateSpeed * Time.fixedDeltaTime)) ;   
            _visualTransform.rotation = newRotation;
        }
    }

    private void OnMove(InputValue value) {
        Vector2 input = value.Get<Vector2>();
        _currentInput = new Vector3(input.x, 0, input.y);
    }
}
