using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _movementSpeed;

    private Rigidbody _rigidbody;
    private Vector3 _currentInput;


    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        _rigidbody.velocity = _currentInput * _movementSpeed;
    }

    void Update()
    {
        
    }

    void OnMove(InputValue value) {
        Vector2 input = value.Get<Vector2>();
        _currentInput = new Vector3(input.x, 0, input.y);
    }
}
