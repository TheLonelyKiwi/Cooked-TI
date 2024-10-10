using System;
using JUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _animationFrequency = 4;
    [SerializeField] private float _animationAmplitude = 0.5f;

    private Player _player;
    private Rigidbody _rigidbody => _player.rigidbody;
    private Vector3 _currentInput;

    public void SetInput(Vector2 input)
    {
        _currentInput = input.ToXZVector3();
    }

    private void Awake() {
        _player = GetComponentInParent<Player>();
    }

    private void FixedUpdate() {
        Vector3 targetVelocity = _currentInput * _movementSpeed;
        Vector3 newVelocity = Vector3.Lerp(_rigidbody.velocity, targetVelocity, 1 - Mathf.Exp(-_acceleration * Time.fixedDeltaTime));
        _rigidbody.velocity = newVelocity;

        if (Vector3.Magnitude(_currentInput) > 0) {
            Quaternion targetRotation = Quaternion.LookRotation(_currentInput);
            Quaternion newRotation = Quaternion.Slerp(_player.rotatingTransform.rotation, targetRotation, 1 - Mathf.Exp(-_rotateSpeed * Time.fixedDeltaTime)) ;   
            _player.rotatingTransform.rotation = newRotation;
        }
    }

    private void Update(){
        Transform visual = _player.visual;
        float movementSpeed = Mathf.Clamp(_rigidbody.velocity.magnitude / _movementSpeed, 0, 1);
        float y = Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI * _animationFrequency));
        y = y * _animationAmplitude;
        y = y * movementSpeed;
        visual.localPosition = new Vector3(0, y, 0);
    }

    private void OnMove(InputValue value) {
        Vector2 input = value.Get<Vector2>();
        _currentInput = new Vector3(input.x, 0, input.y);
    }
}
